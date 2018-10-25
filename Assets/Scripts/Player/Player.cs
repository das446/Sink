using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sink {
	public class Player : NetworkBehaviour {

		public Room curRoom;
		public int money;

		public Inventory inventory;

		public NetworkController networkController;
		public bool local = false;

		public NetworkMovement networkMovement;

		public enum Role { Crew, Saboteur }

		[SyncVar(hook = "OnRoleChange") ]
		public Role role = Role.Crew;

		[SerializeField]
		private LocalPlayer player;

		void Start() {
			inventory = new Inventory();
			curRoom = GameObject.Find("Room1").GetComponent<Room>(); //TODO: Don't use find
			curRoom.Enter(this);
			if (NetworkServer.connections.Count==1) {
				role = Role.Saboteur;
				if (player != null) {
					player.role = role;
					
				}

			}

		}

		public void GetMoney(int amnt) {
			money += amnt;
		}

		public void EnterRoom(Room room, Door door) {
			curRoom.Exit(this);
			room.Enter(this);

			StartCoroutine(WalkThroughDoor(door, room));
		}

		public virtual IEnumerator WalkThroughDoor(Door door, Room room) {
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

		}

		public virtual void EnterRoom(Room room) {

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

		public void SetupNetworking() {

			if (hasAuthority) {
				LocalPlayer player = gameObject.GetComponent<LocalPlayer>();
				player.enabled = true;
				gameObject.GetComponent<PlayerMovement>().enabled = true;
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				gameObject.GetComponent<NetworkMovement>().enabled = false;
				gameObject.transform.GetChild(1).gameObject.SetActive(false);

				this.enabled = false;

			} else {

			}
		}

		public void Win() {

			string r = RoleToInitial();
			CmdSendWinnerOverNetwork(r);
			NetworkManager.singleton.ServerChangeScene("EndScreen");

		}

		public string RoleToInitial() {
			return role == Role.Crew ? "C" : "S";
		}

		[Command]
		public void CmdSendWinnerOverNetwork(string s) {
			RpcSendWinnerOverNetwork(s);
		}

		[ClientRpc]
		public void RpcSendWinnerOverNetwork(string r) {
			PlayerPrefs.SetString("WinnerS", r);
			PlayerPrefs.SetString("Player", r);
		}

		[Command]
		public void CmdUpdatePos(Vector3 p, float rotY) {
			RpcUpdateTargetPos(p, rotY);
		}

		[ClientRpc]
		private void RpcUpdateTargetPos(Vector3 p, float rotY) {
			if (hasAuthority || networkMovement == null) { return; }
			networkMovement.target = p;
			networkMovement.rotY = rotY;
		}

		public void OnRoleChange(Role r){
			Debug.Log("Role changed to "+r.ToString());
		}

	}
}