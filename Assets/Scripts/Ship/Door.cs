using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class Door : MonoBehaviour,Interactable {

		public Room room1, room2;

		public void Interact(Player p){
			Debug.Log("Door");
			if (p.curRoom == room1){
				p.EnterRoom(room2,this);
			}
			else if(p.curRoom == room2){
				p.EnterRoom(room1,this);
			}
		}

		
	}
}