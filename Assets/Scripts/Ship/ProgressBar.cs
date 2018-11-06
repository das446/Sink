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
		public TMPro.TMP_Text text;

		public delegate void OnBarFinish(Player p);

		public event OnBarFinish Finish;

		void Update() {
			if (timeLeft > 0 && inProgress) {
				timeLeft -= Time.deltaTime;

			} else if (timeLeft <= 0 && inProgress) {
				inProgress = false;
				Debug.Log("Finish");
				Finish(player);
			}
			float percent = timeLeft / timeToComplete;
			bar.fillAmount = percent;
			if (inProgress) {
				text.text = Mathf.CeilToInt(timeLeft) + "s";
			}
		}

		public void Activate(Player p) {
			if(inProgress){return;}
			timeLeft = timeToComplete;
			player = p;
			inProgress = true;
		}

		public void Cancel(){
			inProgress = false;
			timeLeft = timeToComplete;
		}

	}
}