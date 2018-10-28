using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	public class ProgressBar : MonoBehaviour {

		public Image bar;
		public float timeToComplete;
		public float timeLeft = 0;
		public bool inProgress;
		public Player player;

		public delegate void OnBarFinish(Player p);

		public event OnBarFinish Finish;

		void Update() {
			if (timeLeft > 0 && inProgress) {
				timeLeft -= Time.deltaTime;
				bar.fillAmount = timeLeft / timeToComplete;
			}
			else if( timeLeft<=0 && inProgress){
				inProgress = false;
				Finish(player);
			}
		}

		public void Activate(Player p) {
			timeLeft = timeToComplete;
			player = p;
		}

	}
}