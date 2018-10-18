using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class ItemMenu : MonoBehaviour,IMenu {

		

		public void Close(LocalPlayer p) {
			gameObject.SetActive(false);
		}

		public void Open(LocalPlayer p) {
			gameObject.SetActive(true);

			//Get items from player
			//You have a grid
			//put the sprites into a grid
		}
	}
}