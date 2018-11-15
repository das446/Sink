using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class SteamBehavior : Interactable {

		public GameObject smoke;
		public GameObject pipe;
		Room room;
		//bool isOn = true;
		//bool enter = true;

		// Use this for initialization
		void Start() {
			smoke.gameObject.SetActive(false);
			//smoke.GetComponent<ParticleSystem>().enableEmission = false;
			//gameObject.SetActive(false);
		}

		// Update is called once per frame
		void Update() {

		}

		public override void DoAction(Player p) {
			smoke.SetActive(true);
		}
	}
}