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

		
		void Start()
		{
			curRoom = GameObject.Find("Room1").GetComponent<Room>();//TODO: Don't use find
			curRoom.Enter(this);
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

		public void SetupNetworking() {

			if (hasAuthority) {
				gameObject.GetComponent<LocalPlayer>().enabled = true;
				gameObject.GetComponent<PlayerMovement>().enabled = true;
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				gameObject.GetComponent<NetworkMovement>().enabled = false;
				this.enabled=false;
			}
		}

		[Command]
		public void CmdUpdatePos(Vector3 p,Vector3 rot) {
			RpcUpdateTargetPos(p,rot);
		}

		[ClientRpc]
		private void RpcUpdateTargetPos(Vector3 p,Vector3 rot) {
			if (hasAuthority || networkMovement==null) { return; }
			networkMovement.target = p;
			networkMovement.rot = rot;
		}
	}
}