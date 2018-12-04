using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {
	public class SteamEvent : ShipEvent {

		public Transform t;
		public GameObject smoke;
		public SteamPipe pipe;

		public bool active;
		public float timer;
		public float baseTime;

		public override void Activate() {
			Debug.Log("Make Smoke");
			smoke.SetActive(true);
			smoke.transform.position = pipe.steamSpawn.position;
			pipe.steamEvent = this;
			active = true;
			timer = baseTime;
			LocalPlayer.singleton.hud.MakeChatMessage("A pipe burst in the boiler room! You have a minute to fix it.");
		}

		public void Stop() {
			smoke.SetActive(false);
			active=false;

		}

		public void Update() {
			if (active) {
				timer -= Time.deltaTime;
				if (timer <= 0 && isServer) {
					Player.EveryoneLoses();
				}
			}

		}

	}
}