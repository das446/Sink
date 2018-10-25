using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class EndScreen : MonoBehaviour {

		public Sprite sink, survive;
		public Image image;
		public Text escapeText, winText;

		void Start()
		{
			string winRole = PlayerPrefs.GetString("WinnerS");
			string playerRole = PlayerPrefs.GetString("Player");
			bool win = CheckWin(winRole,playerRole);
			if(win){
				Win();
			}
			else{
				Lose();
			}
		}

		bool CheckWin(string winRole,string playerRole){
			return winRole == playerRole;
		}

		void Win(){
			
		}
		void Lose(){

		}


	}
}