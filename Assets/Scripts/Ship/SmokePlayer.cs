using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {
	public class SmokePlayer : ShipEvent {

		public Transform t;
		public GameObject smoke;

		public override void Activate() {
			Debug.Log("Make Smoke");
			GameObject s = Instantiate(smoke);
			Destroy(s,30);
		}

	}
}