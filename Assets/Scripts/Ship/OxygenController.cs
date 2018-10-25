using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class OxygenController : Interactable {

		public Room room;

		public override void DoAction(Player p) {
			room.oxygen.setToMax();
		}

        
    }
}