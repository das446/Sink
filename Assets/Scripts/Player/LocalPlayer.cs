using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using Sink.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

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

		/// <summary>
		/// The player prefab has a local and nonlocal component, it then removes the component it's not
		/// </summary>
		protected virtual void OnEnable() {
			singleton = this;
			if (SceneManager.GetActiveScene().name != "SampleScene") { return; }
			curRoom = GameObject.Find(StartRoom).GetComponent<Room>();
			curFloor = GameObject.Find("BottomFloor").GetComponent<Floor>(); //TODO: Don't use find

			movement = GetComponent<PlayerMovement>();
			transform.GetChild(1).gameObject.SetActive(false);

			hud = FindObjectOfType<HUD>(); //TODO: don't use find

			MoveToRoom(curRoom);
			MoveToFloor(curFloor);
			ChangeName(LocalPlayerName);

			if (PhotonNetwork.IsMasterClient) {
				StartCoroutine(SetSab());
			}

			players.Remove(basePlayer);
			Destroy(basePlayer);
			basePlayer = null;

			players.Add(this);
			CloseChat();

			if (model != null) { model.SetActive(false); }
		}

		private void OnCollisionEnter(Collision other) { }

		public void Update() {
			if (gameOver || curFloor == null) { return; }

			CheckInput();

			CheckOutline();
		}

		private void CheckInput() {

			if (Input.GetKeyDown(KeyCode.Tab) && !hud.chatSystem.IsOpen()) {
				OpenChat();

			} else if (Input.GetKeyDown(KeyCode.Tab) && hud.chatSystem.IsOpen()) {
				CloseChat();
			}

			if (Input.GetKeyDown(KeyCode.Mouse1) && MenuOpen) {
				CloseMenu();
			}

			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				movement.LockCursor();
			}

			if (MenuOpen || hud.chatSystem.IsOpen()) { return; }

			
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				CheckInteract();
			} else if (Input.GetKeyDown(KeyCode.D)) {
				DropItem();
			} else if (Input.GetKeyDown(KeyCode.Mouse1)) {
				OpenMenu();
			} else if (Input.GetKeyDown(KeyCode.I)) {
				OpenMenu(itemMenuIndex);
			} else if (Input.GetKeyDown(KeyCode.M)) {
				OpenMenu(mapMenuIndex);
			} else
			if (Input.GetKeyUp(KeyCode.Mouse0)) {
				MouseUp();
			}
			if (Input.GetKeyDown(KeyCode.P)) {
				GameTimer.Pause();
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

		void CheckOutline() { //TODO: Make the logic less of a mess
			//0=close enough
			//1=too far
			//2=close enough but can't 
			if (MenuOpen) { return; }
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange * 10)) {
				IHasOutline ho = hit.collider.gameObject.GetComponent<IHasOutline>();
				Outline o = ho?.GetOutline();
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
			float stuckTimer = 0;
			while (Vector3.Distance(transform.position, target) > 1 && stuckTimer < 0.5f) {
				//TODO: only do the jump at the end if the walk time takes longer than the expected walk time
				transform.position = Vector3.MoveTowards(transform.position, target, WalkThroughDoorSpeed * Time.deltaTime);
				stuckTimer += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			transform.position = target;
			door.gameObject.SetActive(true);
			AutoMove = false;
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
			hud.oxygenBar.UpdateFill();
		}

		public override void SetupNetworking() {

		}

		public void MouseUp() {
			if (OnMouseUp != null) {
				OnMouseUp();
			}
		}

		public override void ChangeRole(Role r) {
			if (!enabled) {
				basePlayer.ChangeRole(r);
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
			yield return new WaitUntil(() => players.Count == PhotonNetwork.CountOfPlayers);
			yield return new WaitForSeconds(3);
			players.RandomItem().ChangeRole(Role.Saboteur);
		}

	}
}