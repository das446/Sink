using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {


	public class ParamsMenu : MonoBehaviour, IMenu {

		LocalPlayer player;
		Room room;

		public void Close(LocalPlayer p) {

		}

		public void Open(LocalPlayer p) {
			player = p;
			//room = p.curRoom; //Doesn't work right now for some reason
		}

		public void ChangeSomething() {
			//player.idk
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}