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

		void Start(){
			if(text==null){
				Debug.Log(name+" has no text");
			}
		}

		void Update() {
			if (timeLeft > 0 && inProgress) {
				timeLeft -= Time.deltaTime;

			} else if (timeLeft <= 0 && inProgress) {
				inProgress = false;
				Finish(player);
			}
			float percent = timeLeft / timeToComplete;
			if (bar != null) {
				bar.fillAmount = percent;
			}
			if (inProgress) {
				text.text = Mathf.CeilToInt(timeLeft) + "s";
			}
		}

		public void Activate(Player p) {
			if (inProgress) { return; }
			timeLeft = timeToComplete;
			player = p;
			inProgress = true;
		}

		public void Cancel() {
			inProgress = false;
			timeLeft = timeToComplete;
		}

		/// <summary>
		/// Sets the text to a certain msg for an amnt of time, then changes it back
		/// </summary>
		/// <param name="msg">message to set it to</param>
		/// <param name="defaultText">message to set it back to afterwards</param>
		/// <param name="time">time in seconds to keep it</param>
		public void DisplayMessage(string msg, string defaultText, int time) {
			text.text = msg;
			this.DoAfterTime(() =>
				text.text = defaultText, time
			);
		}

	}
}