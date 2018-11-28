using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	[System.Serializable]
	public class RandomEvent {

		[Range(0, 100)]
		public int chance;
		public ShipEvent e;

		public void CheckTrigger() {
			int r = Random.Range(0, 100);
			if (chance > r) {
				e?.Trigger();
			}
		}
	}
}