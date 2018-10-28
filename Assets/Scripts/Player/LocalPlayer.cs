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

		protected UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

		public static LocalPlayer singleton;

		private void OnEnable() {
			singleton = this;
			if(SceneManager.GetActiveScene().name=="EndScreen"){return;}
			curRoom = GameObject.Find("Room1").GetComponent<Room>();
			firstPersonController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
			transform.GetChild(1).gameObject.SetActive(false);

			hud = FindObjectOfType<HUD>(); //TODO: don't use find

			transform.position = NetworkManager.singleton.startPositions[0].position;

			EnterRoom(curRoom);
			hud.role.text = role.ToString();

		}

		public void Update() {

			if (Input.GetKeyDown(KeyCode.Mouse0) && !MenuOpen) {
				firstPersonController.LockCursor();
			}

			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Mouse0)) {
				CheckInteract();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && !MenuOpen) {
				OpenMenu();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && MenuOpen) {
				CloseMenu();
			}

			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
				CmdUpdatePos(transform.position, transform.GetChild(1).rotation.eulerAngles.y);
			}
		}

		private void OpenMenu() {
			MenuOpen = true;
			firstPersonController.UnlockCursor();
			hud.Menu.Open();
		}

		private void CloseMenu() {
			MenuOpen = false;
			firstPersonController.LockCursor();
			hud.Menu.Close();
		}

		private void CheckInteract() {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange)) {
				Interactable i = hit.collider.gameObject.GetComponent<Interactable>();
				if (i != null) {
					i.Interact(this);
				}
			}
		}

		public override IEnumerator WalkThroughDoor(Door door, Room room) {
			AutoMove = true;
			Vector3 dir = (door.transform.position - transform.position).normalized * 3;
			Vector3 target = door.transform.position + dir; //TODO: change target to better position
			target.y = transform.position.y;
			float moveSpeed = 2;
			door.gameObject.SetActive(false);
			while (Vector3.Distance(transform.position, target) > 0.5f) {
				transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			door.gameObject.SetActive(true);
			AutoMove = false;
			CmdUpdatePos(transform.position, transform.GetChild(1).rotation.eulerAngles.y);

		}

		public bool CanMove() {
			return !MenuOpen && !AutoMove;
		}

		public override void EnterRoom(Room room) {

			curRoom = room;

			hud.temperatureBar.temperature = room.temperature;
			room.temperature.bar = hud.temperatureBar;
			hud.temperatureBar.update();

			hud.oxygenBar.oxygen = room.oxygen;
			room.oxygen.bar = hud.oxygenBar;
			hud.oxygenBar.update();

			hud.StopCoroutine("FadeRoomName");
			hud.StartCoroutine(hud.FadeRoomName(room));
		}

		/// <summary>
		/// Sends the requested interaction to the server.
		/// </summary>
		/// 
		/// <remarks>
		/// I don't know why I have to call this from the player and can't from the object,
		/// but the way this works is that Interactable.interact() calls this function,
		/// then the player tells the server to tell the clients to do the function.
		/// If other objects can't send Commands by themself and have to put the function here this class will eventualy become a huge mess
		/// with lots of tiny functions that should be called by other classes but can't be. Someone please look into this
		/// </remarks>
		public void SendInteractToServer(Interactable i) {
			Debug.Log(i);
			CmdDoAction(i.gameObject);
		}

		[Command]
		public void CmdDoAction(GameObject i) {
			RpcDoAction(i);
		}

		[ClientRpc]
		public void RpcDoAction(GameObject i) {
			Debug.Log("RpcDoAction");
			if (i == null) {
				Debug.LogError("RpcDoAction called on null gameObject"+i);
				return;
			}
			Interactable interactable = i.GetComponent<Interactable>();
			if (interactable == null) {
				Debug.LogError(i.name + " does not have an Interactable component");
			} else {
				i.GetComponent<Interactable>().DoAction(this);
			}
		}

	}
}