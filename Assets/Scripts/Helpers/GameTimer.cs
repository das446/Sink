using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun;
using Photon.Realtime;
using Sink.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	/// <summary>
	/// Only the server controls the timer, it sends the time to the clients timers 
	/// </summary>
	public class GameTimer : MonoBehaviourPun, IPunObservable {

		[System.Serializable]
		public struct EventAndTime {
			public ShipEvent e;
			public float activationTime;
		}

		public float timeLeft = 360;

		public Text timerText;

		public int curEvent = 0;

		[SerializeField]
		public List<EventAndTime> events;

		public int minPlayerNumber = 1;

		public static bool paused = false;

		///<summary>Does it make more sense for it to be time elapsed or time remaining?</summary>
		/// <param name="minutes">minutes left</param>
		/// <param name="seconds">seconds left</param>
		public delegate void OnTimeAlert(int minutes, int seconds);

		/// <summary>
		/// Subscribe to this event to have a function occur at a certain time
		/// </summary>
		public static event OnTimeAlert TimeLeftAlert;

		int prevMin;
		int prevSec;

		public void Start() {
			Invoke("PlayWarning", 7);
		}

		public void PlayWarning() {
			this.PlaySound("SelfDestructWarning");
		}

		void Update() {
			if (!PhotonNetwork.IsMasterClient) { return; }
			Debug.Log("Tick");
			float t = timeLeft - Time.deltaTime;
			if (timeLeft <= 0) {
				Player.EveryoneLoses();
			}

			// if (events.Count > curEvent) {

			// 	if (timeLeft < events[curEvent].activationTime) {
			// 		events[curEvent].e.Trigger();
			// 		curEvent++;
			// 	}
			// }

			UpdateTimer(t);
		}

		float UpdateTimeLeft(float time) {
			return time;
		}

		void UpdateTimer(float time) {

			timeLeft = time;

			int seconds = (int) time % 60;
			int minutes = (int) time / 60;

			this.photonView.RPC("UpdateDisplay", RpcTarget.All, timeLeft, seconds, minutes);

			if (prevMin == minutes && prevSec == seconds) {
				//because time is a float that's updated every frame it needs to store the previous int val
				//to prevent the event from being called multiple times per second
			} else {
				CheckAlertSounds(seconds, minutes);
				TimeLeftAlert(minutes, seconds);
				prevMin = minutes;
				prevSec = seconds;
			}

		}

		[PunRPC]
		private void UpdateDisplay(int timeLeft, int seconds, int minutes) {
			Debug.Log("UpdateDisplay");
			this.timeLeft = timeLeft;
			string s = seconds >= 10 ? seconds + "" : "0" + seconds;
			string m = minutes >= 10 ? minutes + "" : "0" + minutes;

			timerText.text = minutes + ":" + s;
		}

		private void CheckAlertSounds(int seconds, int minutes) {
			if (minutes == 14 && seconds == 59) {
				this.PlaySound("15Minutes");
			} else if (minutes == 9 && seconds == 59) {
				this.PlaySound("10Minutes");
				//this.PlaySound("10MinuteWarning);
			} else if (minutes == 4 && seconds == 59) {
				this.PlaySound("5Minutes");
				//this.PlaySound("5MinuteWarning");
			} else if (minutes == 0 && seconds == 10) {
				this.PlaySound("Warning");
				//this.PlaySound("10SecWarning");
			} else if (minutes == 0 && seconds == 0) {
				this.PlaySound("ShipIsSinking");
			}
		}

		[PunRPC]
		public static void Pause() {
			paused = !paused;
		}

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		}
	}
}