using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

	[RequireComponent(typeof(ProgressBar))]
	public class OxygenController : Interactable {

		public Room room;
		public Item refItem;
		public int refItemAmnt = 1;

		public ProgressBar bar;

		public TMPro.TMP_Text text;

		void Start() {
			bar.text = text;
			bar.Finish += OnBarFinish;
		}

		public override void DoAction(Player p) {

			Debug.Log(p);
			int size = p.inventory.items[refItem];

			if (size >= refItemAmnt) {
				p.inventory.UseItem(refItem);
				bar.Activate(p);
				
			} else {
				text.text = "Requires " + refItemAmnt + " " + refItem.name + Plural();
				this.DoAfterTime(
					() => text.text = "Oxygen", 3
				);
			}

		}
		public string Plural() { return refItemAmnt == 1 ? "" : "s"; }

		public void OnBarFinish(Player p) {
			Debug.Log("OnBarFinish");
			room.oxygen.setToMax();
			text.text = "Oxygen";
		}

	}
}