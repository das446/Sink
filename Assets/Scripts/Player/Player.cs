using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class Player : MonoBehaviour {

		public bool MenuOpen;
		public Room curRoom;
		public int money;
		public bool GoingThroughDoor;

		public Inventory inventory;

		[SerializeField]
		public HUD hud;

		public float interactRange = 2;

		UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

		public void Start() {
			curRoom.Enter(this);
		}

		public void Update() {

			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Mouse0)) {
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange)) {
					Interactable i = hit.collider.gameObject.GetComponent<Interactable>();
					if (i != null) {
						i.Interact(this);
					}
				}
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && !MenuOpen) {
				OpenMenu();
			} else if (Input.GetKeyDown(KeyCode.Mouse1) && MenuOpen) {
				CloseMenu();
			}
		}

		private void OpenMenu() {
			MenuOpen = true;
		}

		private void CloseMenu() {
			MenuOpen = false;
		}

		public void EnterRoom(Room room, Door door) {
			curRoom.Exit(this);
			room.Enter(this);

			StartCoroutine(WalkThroughDoor(door, room));
		}

		public IEnumerator WalkThroughDoor(Door door, Room room) {
			GoingThroughDoor = true;
			Vector3 dir = (door.transform.position - transform.position).normalized * 3;
			Vector3 target = door.transform.position + dir;//TODO: change target to better position
			target.y = transform.position.y;
			float moveSpeed = 2;
			door.gameObject.SetActive(false);
			while (Vector3.Distance(transform.position, target) > 0.5f) {
				transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			door.gameObject.SetActive(true);

			GoingThroughDoor = false;
		}

		public bool CanMove() {
			return !MenuOpen && !GoingThroughDoor;
		}

	}
}