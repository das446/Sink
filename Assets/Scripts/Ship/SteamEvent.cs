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

		public static List<SteamPipe> activeEvents=new List<SteamPipe>();

		public override void Activate() {
			Debug.Log("Make Smoke");
			GameObject newSmoke = Instantiate(smoke);
			newSmoke.SetActive(true);
			newSmoke.transform.position = pipe.steamSpawn.position;
			pipe.steamEvent = this;
			pipe.smoke = newSmoke;
			active = true;
			timer = baseTime;
			LocalPlayer.singleton.hud.MakeChatMessage("A pipe burst in the boiler room! You have a minute to fix it.");
			activeEvents.Add(pipe);
		}

		public void Stop() {
			smoke.SetActive(false);
			active=false;
			activeEvents.Remove(pipe);
			Destroy(pipe.smoke);
			LocalPlayer.singleton.hud.MakeChatMessage("Pipe fixed");
		}

		public void Initialize(){

		}

	}
}