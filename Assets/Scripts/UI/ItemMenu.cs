using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class ItemMenu : MonoBehaviour,IMenu {

		public Inventory inventory;

		void Start()
		{
			inventory = new Inventory();
		}

		public void Close(LocalPlayer p) {
			gameObject.SetActive(false);
		}

		public void Open(LocalPlayer p) {
			gameObject.SetActive(true);

			inventory = p.inventory; /// Temp show of it working
			inventory.PrintInv();

			


			//Get items from player
			//You have a grid
			//put the sprites into a grid
		}
	}
}