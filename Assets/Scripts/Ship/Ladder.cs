using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class Ladder : Interactable {

		public Room upperRoom, lowerRoom;
		public Floor upperFloor, lowerFloor;
		public Transform top, bottom;

		public override void DoAction(Player p) {
			if (p.curFloor == lowerFloor) {
				p.StartCoroutine(p.ClimbLadder(this, upperRoom, upperFloor));
			} else if(p.curFloor == upperFloor) {
				p.StartCoroutine(p.ClimbLadder(this, lowerRoom, lowerFloor));
			}
			
		}
	}
}