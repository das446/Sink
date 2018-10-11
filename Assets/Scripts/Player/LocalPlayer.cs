using System;
using System.Collections;
using System.Collections.Generic;
using Sink.Network;
using UnityEngine;

namespace Sink {

	/// <summary>
	/// Player functions that are only used by the user client
	/// </summary>
	public class LocalPlayer : Player {

		public bool MenuOpen;
		public bool AutoMove;

		[SerializeField]
		public HUD hud;

		public float interactRange = 2;

		public void Start() {
			firstPersonController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
			
		}


		private string PosToNetString() {
			float x = transform.position.x;
			float y = transform.position.y;
			float z = transform.position.z;
			string s = "Move|" + name + "|" + x + "|" + y + "|" + z;
			return s;
		}

		public void Update() {

			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Mouse0)) {
				CheckInteract();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && !MenuOpen) {
				OpenMenu();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && MenuOpen) {
				CloseMenu();
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

		public IEnumerator WalkThroughDoor(Door door, Room room) {
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
		}

		public bool CanMove() {
			return !MenuOpen && !AutoMove;
		}

		public override void EnterRoom(Room room) {
			curRoom = room;
			room.players.Add(this);

			hud.temperatureBar.temperature = room.temperature;
			room.temperature.bar = hud.temperatureBar;
			hud.temperatureBar.update();

			hud.oxygenBar.oxygen = room.oxygen;
			room.oxygen.bar = hud.oxygenBar;
			hud.oxygenBar.update();

			hud.StopCoroutine("FadeRoomName");
			hud.StartCoroutine(hud.FadeRoomName(room));
		}

	}
}