using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class Ladder : Interactable {

		public Room upper, lower;
		public Transform top, bottom;

		public override void DoAction(Player p) {
            Debug.Log("Ladder, playerRoom = "+p.curRoom);
			if (p.curRoom == lower) {
				p.StartCoroutine(p.ClimbLadder(this, upper));
			} else if (p.curRoom == upper) {
				p.StartCoroutine(p.ClimbLadder(this, lower));
			}
		}
	}
}