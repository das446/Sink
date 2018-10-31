using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {
	public class Inventory {

		/// <summary>
		/// key = item, val = amount
		/// </summary>
		public Dictionary<Item, int> items;

		public Text invText;

		public Inventory() {
			items = new Dictionary<Item, int>();
		}

		void Update() {

			PrintInv();
			/* 
			if ( items.Count > 0){
			Debug.Log( items.ElementAt(0))	;
			}
			*/
		}

		public void GetItem(Item i, int amount = 1) {
			Item it = items.Keys.Where(x => x.name == i.name).FirstOrDefault();
			if (it != null) {
				items[it] += amount;
			} else {
				items.Add(i, amount);
			}
		}

		public void UseItem(Item i) {
			Item it = items.Keys.Where(x => x.name == i.name).FirstOrDefault();
			if (it != null) {
				items[it]--;
			}
		}

		public void SpendItem(Item i) {
			Item it = items.Keys.Where(x => x.name == i.name).FirstOrDefault();
			if (it != null) {
				items.Remove(it);
			}
		}

		public void PrintInv() // Temp solution to ui
		{

			string curInv = "No items";

			if (items.Count > 0) // change once more item types are added 
			{
				int count = 0;
				count = items.Count - 1;
				curInv = "Gears : " + count;
				/* 
				//int placeholder
				for (int i = 0; i < items.Count; i++)
				{
					
				}
				// */

			}

			invText.text = curInv;
		}

		public void Drop(Item item, int amnt = 1) {
			if (items.ContainsKey(item)) {
				if (amnt > items[item]) { amnt = items[item]; }
				items[item] -= amnt;
			}
		}

		public Dictionary<Item, int> GetInv() {
			return items;
		}

		public int this [Item i] {
			get {
				if (items.ContainsKey(i)) {
					return items[i];
				} else {
					items.Add(i, 0);
					return items[i];
				}
			}
		}

	}
}