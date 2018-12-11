using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Sink.Audio;

namespace Sink {
	public class SteamEvent : ShipEvent {

		public SteamPipe pipe;

		public bool active;
		public float timer;
		public float baseTime;

		public static List<SteamPipe> activeEvents=new List<SteamPipe>();

		public override void Activate() {
			Debug.Log("Make Smoke");
			pipe.MakeSteam(this);
			active = true;
			timer = baseTime;
			LocalPlayer.singleton.hud.MakeChatMessage("A pipe burst in the boiler room! You have a minute to fix it.");
			activeEvents.Add(pipe);
			this.PlaySound("SteamRelease");
		}

		public void Stop() {
			pipe.StopSteam();
			active=false;
			activeEvents.Remove(pipe);
			LocalPlayer.singleton.hud.MakeChatMessage("Pipe fixed");
		}

		public void Initialize(){

		}

	}
}