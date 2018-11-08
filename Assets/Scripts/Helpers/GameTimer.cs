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

		public double timeLeft;

		public Text text;

		public int curEvent = 0;

		[SerializeField]
		public List<EventAndTime> events;

		void Update() {
			timeLeft -= Time.deltaTime;
			text.text = timeLeft + "s";
			if (timeLeft <= 0) {
				Player.Win(Player.Role.Crew);
			}

			if(timeLeft<events[curEvent].activationTime){
				events[curEvent].e.Activate();
				curEvent++;
			}
		}

	}
}