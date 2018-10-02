using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class OxygenController : MonoBehaviour, Interactable {

		public Room room;

		public void Interact(Player p) {
			room.oxygen.setToMax();
		}

	}
}