using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Sink.Audio;

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

		void Start()
		{
			this.PlaySound("SelfDestructWarning");
			//Doesnt work for some reason
		}

		void Update() {
			if (!isServer || NetworkServer.connections.Count < minPlayerNumber) { return; }
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


			if(minutes == 14 && seconds == 59)
			{
				this.PlaySound("15Minutes");
			}
			else if(minutes == 9 && seconds == 59)
			{
				//this.PlaySound("10MinuteWarning");
				this.PlaySound("10Minutes");
			}
			else if(minutes == 4 && seconds == 59)
			{
				//this.PlaySound("5MinuteWarning");
				this.PlaySound("5Minutes");
			}
			else if(minutes == 1 && seconds == 59)
			{
				this.PlaySound("2MinuteWarning");
			}
			else if(minutes == 0 && seconds == 59)
			{
				this.PlaySound("1MinuteWarning");
			}
			else if(minutes == 0 && seconds == 30)
			{
				this.PlaySound("30SecWarning");
			}
			else if(minutes == 0 && seconds == 10)
			{
				this.PlaySound("10SecWarning");
			}
			else if(minutes == 0 & seconds == 0)
			{
				this.PlaySound("ShipIsSinking");
			}
		}

	}
}