using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sink {

	public class GameTimer : NetworkBehaviour {

		[System.Serializable]
		public struct EventAndTime {
			public ShipEvent e;
			public float activationTime;
		}

		[SyncVar(hook = "UpdateTimer")]
		public float timeLeft = 360;

		public Text timerText;

		public int curEvent = 0;

		[SerializeField]
		public List<EventAndTime> events;

		public int minPlayerNumber = 1;

		public static bool paused = false;

		void Update() {
			if (!isServer || NetworkServer.connections.Count < minPlayerNumber || paused) { return; }
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0) {
				Player.EveryoneLoses();
			}

			if (events.Count > curEvent) {

				if (timeLeft < events[curEvent].activationTime) {
					events[curEvent].e.Trigger();
					curEvent++;
				}
			}

			UpdateTimer(timeLeft);
		}

		public void UpdateTimer(float time) {

			int seconds = (int) time % 60;
			int minutes = (int) time / 60;

			string s = seconds >= 10 ? seconds + "" : "0" + seconds;
			string m = minutes >= 10 ? minutes + "" : "0" + minutes;

			timerText.text = minutes + ":" + s;
		}

		public static void Pause() {
			paused = !paused;
		}

	}
}