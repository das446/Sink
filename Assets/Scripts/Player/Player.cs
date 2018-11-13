using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sink {
	public class Player : NetworkBehaviour {

		public Room curRoom;
		public Floor curFloor;
		public int money;

		public Inventory inventory;

		public NetworkController networkController;
		public bool local = false;

		public string StartRoom;

		public NetworkMovement networkMovement;

		public bool locked=false;

		public enum Role { Crew, Saboteur }

		[SyncVar(hook = "OnRoleChange")]
		public Role role = Role.Crew;

		[SerializeField]
		private LocalPlayer player;

		public float WalkThroughDoorSpeed = 50;
		public float ClimbLadderSpeed = 50;

		public new Collider collider;

		public CharacterController cc;




		protected virtual void Start() {
			if (SceneManager.GetActiveScene().name == "EndScreen") { return; }
			inventory = new Inventory();
			curRoom = GameObject.Find(StartRoom).GetComponent<Room>(); //TODO: Don't use find
			curFloor = GameObject.Find("BottomFloor").GetComponent<Floor>(); //TODO: Don't use find
			curRoom.Enter(this);
			if (NetworkServer.connections.Count == 1) {
				role = Role.Saboteur;
				if (player != null) {
					player.role = role;
				}

			}

		}

		public void GetMoney(int amnt) {
			money += amnt;
		}

		public virtual void MoveToRoom(Room room) {
			curRoom.Exit(this);
			room.Enter(this);
			curRoom = room;
		}

		public virtual void MoveToFloor(Floor floor){
			curFloor = floor;
		}

		public virtual IEnumerator WalkThroughDoor(Door door, Room room) {
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
		}

		public virtual IEnumerator ClimbLadder(Ladder ladder, Room room, Floor floor){
			MoveToRoom(room);
			MoveToFloor(floor);
			Vector3 target;
			if (curRoom == ladder.upperRoom) {
				target = ladder.bottom.position;
			} else {
				target = ladder.top.position;
			}
			while (Vector3.Distance(transform.position, target) > 0.5f) {
				transform.position = Vector3.MoveTowards(transform.position, target, ClimbLadderSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			NetworkController.singleton.CmdUpdatePos(transform.position, transform.GetChild(1).rotation.eulerAngles.y, gameObject);
			
		}

		public virtual void RecieveMove(string s) {

		}

		public virtual void Setup() {

		}

		public override void OnStartAuthority() {
			SetupNetworking();
		}

		public void GetItem(Item item) {
			inventory.GetItem(item);
		}

		public virtual void Lose() {

		}

		public virtual void SetupNetworking() {

			if (hasAuthority) {
				LocalPlayer player = gameObject.GetComponent<LocalPlayer>();
				player.enabled = true;
				gameObject.GetComponent<PlayerMovement>().enabled = true;
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				Destroy(GetComponent<NetworkMovement>());
				gameObject.transform.GetChild(1).gameObject.SetActive(false);
				enabled = false;

			} else {
				Destroy(GetComponent<LocalPlayer>());

			}
		}

		public void Win() {
			string playerRole = RoleToInitial(role);
			NetworkController.singleton.CmdSendWinnerOverNetwork(playerRole);
			NetworkManager.singleton.ServerChangeScene("EndScreen");
		}

		public static void Win(Role r){
			string playerRole = RoleToInitial(r);
			NetworkController.singleton.CmdSendWinnerOverNetwork(playerRole);
			NetworkManager.singleton.ServerChangeScene("EndScreen");
		}


		public string RoleToInitial() {
			return role == Role.Crew ? "C" : "S";
		}

		public static string RoleToInitial(Role r) {
			return r == Role.Crew ? "C" : "S";
		}

		public void UpdateTargetPos(Vector3 p, float rotY) {
			if (hasAuthority || networkMovement == null) { return; }
			networkMovement.target = p;
			networkMovement.rotY = rotY;
		}

		public void OnRoleChange(Role r) {
			Debug.Log("Role changed to " + r.ToString());
		}



	}
}