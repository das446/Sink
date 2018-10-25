using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class EndScreen : MonoBehaviour {

		public Sprite sink, survive;
		public Image image;
		public Text resultText;

		void Start() {
			string winRole = PlayerPrefs.GetString("WinnerS");
			Debug.Log(winRole);
			string playerRole = LocalPlayer.singleton.RoleToInitial();
			Debug.Log(playerRole);
			if (winRole == "C" && playerRole == "C") {
				WinC();
			} else if (winRole == "S" && playerRole == "C") {
				LoseC();
			} else if (winRole == "S" && playerRole == "S") {
				WinS();
			} else if (winRole == "C" && playerRole == "S") {
				LoseS();
			}
			//LocalPlayer.singleton.hud.enabled = false;
		}

		bool CheckWin(string winRole, string playerRole) {
			return winRole == playerRole;
		}

		void WinC() {
			image.sprite = survive;
			resultText.text = "You Win!\nYou survived and found the saboteur.";
		}
		void LoseC() {
			image.sprite = sink;
			resultText.text = "You Lose!\nYou sank with the ship and the saboteur got away.";
		}

		void WinS() {
			image.sprite = sink;
			resultText.text = "You Win!\nYou were able to sink the ship and escape.";
		}
		void LoseS() {
			image.sprite = survive;
			resultText.text = "You Lose!\nYou were captured and the ship was able to surface.";
		}

	}
}