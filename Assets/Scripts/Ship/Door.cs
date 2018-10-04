using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class Door : Interactable {

		public Room room1, room2;

		protected override void DoAction(Player p){
			if (p.curRoom == room1){
				p.EnterRoom(room2,this);
			}
			else if(p.curRoom == room2){
				p.EnterRoom(room1,this);
			}
		}
    }
}