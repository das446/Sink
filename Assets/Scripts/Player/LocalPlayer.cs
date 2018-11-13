using System;
using System.Collections;
using System.Collections.Generic;
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

		public Rigidbody rb;

		protected virtual void OnEnable() {
			singleton = this;
			if (SceneManager.GetActiveScene().name == "EndScreen") { return; }
			inventory = new Inventory();
			curRoom = GameObject.Find(StartRoom).GetComponent<Room>();
			curFloor = GameObject.Find("BottomFloor").GetComponent<Floor>(); //TODO: Don't use find

			movement = GetComponent<PlayerMovement>();
			transform.GetChild(1).gameObject.SetActive(false);

			hud = FindObjectOfType<HUD>(); //TODO: don't use find

			transform.position = NetworkManager.singleton.startPositions[0].position;

			MoveToRoom(curRoom);
			MoveToFloor(curFloor);
			hud.role.text = role.ToString();

		}

		private void OnCollisionEnter(Collision other) {
			Debug.Log(other.gameObject.name);
		}


		public void Update() {

			if (Input.GetKeyDown(KeyCode.Mouse0) && !MenuOpen) {
				movement.LockCursor();
			}

			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Mouse0)) {
				CheckInteract();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && !MenuOpen) {
				OpenMenu();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && MenuOpen) {
				CloseMenu();
			} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
				MouseUp();
			}

			NetworkController.singleton.CmdUpdatePos(transform.position, transform.GetChild(1).rotation.eulerAngles.y, gameObject);

		}

		/// <summary>
		/// Open menu and lock player
		/// </summary>
		private void OpenMenu() {
			MenuOpen = true;
			movement.UnlockCursor();
			hud.Menu.Open(this);
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
			return !MenuOpen && !AutoMove && !locked;
		}

		public override void MoveToRoom(Room room) {

			curRoom?.Exit(this);
			room?.Enter(this);
			curRoom = room;

			//hud.StopCoroutine("FadeRoomName");
			//hud.StartCoroutine(hud.FadeRoomName(room));
		}

		public override void MoveToFloor(Floor floor){

			curFloor = floor;

			hud.temperatureBar.temperature = floor.temperature;
			curFloor.temperature.bar = hud.temperatureBar;
			hud.temperatureBar.update();

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

	}
}