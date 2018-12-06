using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class SteamPipe : Interactable {

		public SteamEvent steamEvent;
		public Transform steamSpawn;
		public Item item;
		public ProgressBar bar;
		public GameObject steam;

		public override void DoAction(Player p) {
			if (steamEvent == null) { return; }
			if (!steamEvent.active) { return; }
			if (p.inventory[item] <= 0) {
				bar.DisplayMessage("Requires 1 " + item.name, "", 2);
			} else if (bar.inProgress) {
				return;
			} else {
				p.inventory.UseItem(item);
				steamEvent.Stop();
			}
		}

		public void MakeSteam(SteamEvent se) {
			steamEvent = se;
			steam.SetActive(true);
		}

		public void StopSteam() {
			steam.SetActive(false);
			steamEvent = null;
		}
	}
}