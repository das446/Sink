using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
    public class EscapePod : Interactable {

        public Item refItem;
        public int itemsLeft = 1;
        public ProgressBar bar;
        public TMPro.TMP_Text text;

        // Use this for initialization
        void Start() {
            bar.text = text;
            bar.Finish += OnBarFinish;
            text.text = "Escape Pod " + itemsLeft + " parts left";
        }

        public override bool CanInteract(Player p) {
            return p.curFloor.oxygen.curOx > 0;
        }

        // Update is called once per frame
        void Update() {

        }
        public override void DoAction(Player p) {

            if (!CanInteract(p)) {
                bar.DisplayMessage("Too low on oxygen", "Escape Pod - " + itemsLeft + " parts left", 1);
            } else if (p.inventory[refItem] <= 0) {
                bar.DisplayMessage("Requires 1 Gear", "Escape Pod - " + itemsLeft + " parts left", 1);
            } else if (p.role == Player.Role.Saboteur && !bar.inProgress) {
                bar.Activate(p);
            }
        }
        public void OnBarFinish(Player p) {
            itemsLeft--;
            text.text = "Escape Pod" + itemsLeft + " parts left";
            if (itemsLeft == 0) {
                p.Win();
            } else {
                p.inventory.UseItem(refItem);
            }
        }
    }
}