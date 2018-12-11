using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Sink {

	/// <summary>
	/// Player functions that are only used by the user client
	/// </summary>
	public class LocalPlayer : Player {

		public bool MenuOpen;
		public bool AutoMove;

		[SerializeField]
		public HUD hud;

		public float interactRange = 4;

		public PlayerMovement movement;

		public static LocalPlayer singleton;

		public static event Action OnMouseUp;

		public static string LocalPlayerName;

		public Rigidbody rb;

		public Player basePlayer;

		int itemMenuIndex = 6;
		int mapMenuIndex = 7;

		Outline curOutline;
		public int outlineDist;

		public GameObject model;

		protected virtual void OnEnable() {
			singleton = this;
			if (SceneManager.GetActiveScene().name != "SampleScene") { return; }
			inventory = new Inventory();
			curRoom = GameObject.Find(StartRoom).GetComponent<Room>();
			curFloor = GameObject.Find("BottomFloor").GetComponent<Floor>(); //TODO: Don't use find

			movement = GetComponent<PlayerMovement>();
			transform.GetChild(1).gameObject.SetActive(false);

			hud = FindObjectOfType<HUD>(); //TODO: don't use find

			transform.position = NetworkManager.singleton.startPositions[0].position;

			MoveToRoom(curRoom);
			MoveToFloor(curFloor);
			ChangeName(LocalPlayerName);

			hud.hotbar.GetLocalPlayer(this);
			hud.hotbar.Activate();

			if (isServer) {
				StartCoroutine(SetSab());
			}

			players.Remove(basePlayer);
			Destroy(basePlayer);
			basePlayer = null;

			players.Add(this);
			CloseChat();
			model.SetActive(false);
		}

		private void OnCollisionEnter(Collision other) { }

		public void Update() {
			if (gameOver || curFloor == null) { return; }

			CheckInput();

			CheckOutline();

			NetworkController.singleton.CmdUpdatePos(transform.position, transform.GetChild(1).rotation.eulerAngles.y, gameObject);

		}

		private void CheckInput() {
			if (Input.GetKeyDown(KeyCode.Mouse0) && !MenuOpen && !hud.chatSystem.IsOpen()) {
				movement.LockCursor();
			}

			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Mouse0)) {
				CheckInteract();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && !MenuOpen && !hud.chatSystem.IsOpen()) {
				OpenMenu();
			} else if (Input.GetKeyDown(KeyCode.I) && !MenuOpen && !hud.chatSystem.IsOpen()) {
				OpenMenu(itemMenuIndex);
			} else if (Input.GetKeyDown(KeyCode.M) && !MenuOpen && !hud.chatSystem.IsOpen()) {
				OpenMenu(mapMenuIndex);
			} else
			if (Input.GetKeyDown(KeyCode.Mouse1) && MenuOpen) {
				CloseMenu();
			} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
				MouseUp();
			}

			if (Input.GetKeyDown(KeyCode.Tab) && !hud.chatSystem.IsOpen()) {
				OpenChat();

			} else if (Input.GetKeyDown(KeyCode.Tab) && hud.chatSystem.IsOpen()) {
				CloseChat();

			}
		}

		public void CloseChat() {
			movement.LockCursor();
			singleton.movement.enabled = true;
			hud.chatSystem.ForceCloseChat();
		}

		public void OpenChat() {
			movement.UnlockCursor();
			singleton.movement.enabled = false;
			hud.chatSystem.OpenChat(true, 0);
		}

		/// <summary>
		/// Open menu and lock player
		/// </summary>
		private void OpenMenu() {
			MenuOpen = true;
			movement.UnlockCursor();
			hud.Menu.Open(this);
		}

		/// <summary>
		/// Open menu and lock player
		/// </summary>
		/// <param name="index">index of menu to open</param>
		private void OpenMenu(int index) {
			MenuOpen = true;
			movement.UnlockCursor();
			hud.Menu.Open(this, index);
		}

		private void CloseMenu() {
			MenuOpen = false;
			movement.LockCursor();
			hud.Menu.Close();
		}

		private void CheckInteract() {
			if (MenuOpen) { return; }
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange)) {
				Interactable i = hit.collider.gameObject.GetComponent<Interactable>();
				if (i != null) {
					i.Interact(this);
				}
			}
		}

		private void CheckOutline() { //TODO: Make the logic less of a mess
			if (MenuOpen) { return; }
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange * 10)) {
				Outline o = hit.collider.gameObject.GetComponent<IHasOutline>()?.GetOutline();
				if (o == null) {
					if (curOutline != null) {
						curOutline.enabled = false;
					}
					curOutline = null;
				} else if (o != curOutline) {
					if (curOutline != null) {
						curOutline.enabled = false;
					}
					curOutline = o;
					if (hit.distance <= interactRange) {
						curOutline.color = 0;
					} else {
						curOutline.color = 1;
					}
					curOutline.enabled = true;
				} else if (curOutline != null) {
					if (hit.distance <= interactRange) {
						curOutline.color = 0;
					} else {
						curOutline.color = 1;
					}
				}
			} else if (curOutline != null) {
				curOutline.enabled = false;
				curOutline = null;
			}
		}

		public override IEnumerator WalkThroughDoor(Door door, Room room) {
			AutoMove = true;
			MoveToRoom(room);
			Vector3 dir = (door.transform.position - transform.position).normalized * 3;
			Vector3 target = door.transform.position + dir; //TODO: change target to better position
			target.y = transform.position.y;
			door.gameObject.SetActive(false);
			while (Vector3.Distance(transform.position, target) > 0.5f) {
				transform.position = Vector3.MoveTowards(transform.position, target, WalkThroughDoorSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			door.gameObject.SetActive(true);
			AutoMove = false;
			NetworkController.singleton.CmdUpdatePos(transform.position, transform.GetChild(1).rotation.eulerAngles.y, gameObject);

		}

		public override IEnumerator ClimbLadder(Ladder ladder, Room room, Floor floor) {
			if (AutoMove) { yield break; } //cant start climbing ladder if already climbing
			MoveToRoom(room);
			MoveToFloor(floor);
			AutoMove = true;
			Vector3 target = transform.position;
			if (curRoom == ladder.upperRoom) {
				target.y = ladder.top.position.y;

			} else {
				target.y = ladder.bottom.position.y;
			}
			rb.useGravity = false;
			movement.enabled = false;
			//collider.enabled=false;
			while (Vector3.Distance(transform.position, target) > 0.5f) {
				transform.position = Vector3.MoveTowards(transform.position, target, ClimbLadderSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			//collider.enabled=true;
			movement.enabled = true;
			rb.useGravity = true;
			AutoMove = false;
			NetworkController.singleton.CmdUpdatePos(transform.position, transform.GetChild(1).rotation.eulerAngles.y, gameObject);

		}

		public bool CanMove() {
			return !MenuOpen && !AutoMove && !locked & !searching;
		}

		public override void MoveToRoom(Room room) {

			curRoom?.Exit(this);
			room?.Enter(this);
			curRoom = room;

			//hud.StopCoroutine("FadeRoomName");
			//hud.StartCoroutine(hud.FadeRoomName(room));
		}

		public override void MoveToFloor(Floor floor) {

			curFloor = floor;

			hud.oxygenBar.oxygen = floor.oxygen;
			curFloor.oxygen.bar = hud.oxygenBar;
			hud.oxygenBar.update();
		}

		public override void SetupNetworking() {

		}

		public void MouseUp() {
			if (OnMouseUp != null) {
				OnMouseUp();
			}
		}

		public override void OnChangeRole(Role r) {
			if (!enabled) {
				basePlayer.OnChangeRole(r);
				players.Remove(this);
				Destroy(this);
			} else {
				role = r;
				if (r == Role.Saboteur) {
					if (hud == null) { hud = FindObjectOfType<HUD>(); }
					hud.playerFace.sprite = hud.sabHead;
					hud.playerCircle.sprite = hud.sabCircle;
				}
			}
		}

		public IEnumerator SetSab() {
			yield return new WaitUntil(() => players.Count == NetworkServer.connections.Count);
			yield return new WaitForSeconds(3);
			players.RandomItem().ChangeRole(Role.Saboteur);

			yield return new WaitForSeconds(3);

		}

	}
}