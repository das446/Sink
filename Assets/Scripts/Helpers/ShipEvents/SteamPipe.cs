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
			if (p.item == item && !bar.inProgress) {
				p.UseItem(item);
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