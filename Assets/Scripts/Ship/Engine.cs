using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class Engine : Interactable { 

		public override void DoAction(Player p) {
			if (p.curFloor.oxygen.curOx > 0 && SteamEvent.activeEvents.Count == 0) {
				//p.inventory.UseItem(battery);
				p.Win();
			}
		}
	}
}