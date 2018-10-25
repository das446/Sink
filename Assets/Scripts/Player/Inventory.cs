using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sink {
	public class Inventory {

		/// <summary>
		/// key = item, val = amount
		/// </summary>
		public Dictionary<Item, int> items;

		public Inventory() {
			items = new Dictionary<Item, int>();
		}
		
		

		void Update()
		{
			if ( items.Count > 0){
			Debug.Log(items.ElementAt(0))	;
			}
		}

		public void GetItem(Item i, int amount = 1) {
			Item it = items.Keys.Where(x => x.name == i.name).FirstOrDefault();
			if (it!=null) {
				items[it]+=amount;
			}
			else{
				items.Add(i,amount);
			}
		}

		public void UseItem(Item i){
			Item it = items.Keys.Where(x => x.name == i.name).FirstOrDefault();
			if(it!=null){
				items[it]--;
			}
		}

		public Dictionary<Item, int> GetInv()
		{
			return items;
		}

	}
}