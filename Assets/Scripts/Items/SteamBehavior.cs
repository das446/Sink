using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class SteamBehavior : MonoBehaviour {
		public int rng;
		public GameObject smoke;
		public GameObject pipe;
		Room room;
		//bool isOn = true;
		//bool enter = true;

		void Start() {
			rng = Random.Range(0, 4);
			smoke.gameObject.SetActive(false);
			//smoke.GetComponent<ParticleSystem>().enableEmission = false;
			//gameObject.SetActive(false);
		}

	}
}