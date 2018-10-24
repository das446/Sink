using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class Engine : Interactable {

		public Item battery;

		public override void DoAction(Player p){
			if(p.inventory[battery]>0){
				p.inventory.UseItem(battery);
				p.Win();
			}
		}
	}
}