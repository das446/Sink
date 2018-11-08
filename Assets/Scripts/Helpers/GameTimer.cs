using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sink {

	public class GameTimer : NetworkBehaviour {

		public double timeLeft;

		public Text text;

		void Update() {
			timeLeft -= Time.deltaTime;
			text.text = timeLeft + "s";
			if (timeLeft <= 0) {
				//Player.Win(Player.Role.Crew);
			 }
		}

	}
}