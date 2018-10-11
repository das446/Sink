using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class Player : MonoBehaviour {

		
		public Room curRoom;
		public int money;
		

		public Inventory inventory;

		public static List<Player> players = new List<Player>();


		protected UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

		public void GetMoney(int amnt){
			money+=amnt;
		}

		public void EnterRoom(Room room, Door door) {
			curRoom.Exit(this);
			room.Enter(this);

			StartCoroutine(WalkThroughDoor(door, room));
		}

		public IEnumerator WalkThroughDoor(Door door, Room room) {
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

		public virtual void EnterRoom(Room room){

		}

		public virtual void RecieveMove(string s){

		}

		public virtual void Setup(){
			
		}

		

		

		

	}
}