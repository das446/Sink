using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sink {

	public class GameTimer : NetworkBehaviour {

		[System.Serializable]
		public struct EventAndTime{
			public ShipEvent e;
			public float activationTime;
		}

		[SyncVar(hook="UpdateTimer")]
		public float timeLeft = 360;

		public Text timerText;

		public int curEvent = 0;

		[SerializeField]
		public List<EventAndTime> events;

		void Update() {
			if(!isServer || NetworkServer.connections.Count<2){return;}
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0) {
				Player.Win(Player.Role.Crew);
			}

			if(timeLeft<events[curEvent].activationTime){
				events[curEvent].e.Activate();
				curEvent++;
			}

			UpdateTimer(timeLeft);
		}

		public void UpdateTimer(float time){

			int seconds = (int)time%60;
			int minutes = (int)time/60;

			timerText.text = minutes + ":"+seconds;
		}

	}
}