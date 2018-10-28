using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

	[RequireComponent(typeof(ProgressBar))]
	public class OxygenController : Interactable {

		public Room room;
		public Item refItem;
		public int refItemAmnt;

		public ProgressBar bar;

		public TMPro.TMP_Text text;

		void Start() {

		}

		public override void DoAction(Player p) {

			// When multiple types of items get introduced,
			// each 'reference' item will given the name of the,
			// item it uses to be repaired which it will search for and remove and instance of.
			int size = p.inventory.items.Count;

			if (size >= refItemAmnt) {
				bar.Activate(p);
				bar.Finish+=OnBarFinish;

			} else {
				text.text = "Requires " + refItemAmnt + " " + refItem + Plural;
			}

		}
        public string Plural => refItemAmnt == 1 ? "" : "s";

        public void OnBarFinish(Player p) {
			room.oxygen.setToMax();
		}

	}
}