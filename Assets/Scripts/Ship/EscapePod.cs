using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sink.Audio;
using static Sink.Interactable;

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
                p.locked=true;
                bar.Activate(p);
                PlaySoundLocalOnly("Assembly",p);
            }
        }
        public void OnBarFinish(Player p) {
            itemsLeft--;
            text.text = "Escape Pod" + itemsLeft + " parts left";
            if (itemsLeft == 0) {
                this.PlaySoundLocalOnly("EscapePod", p);
                p.Win();
            } else {
                p.inventory.UseItem(refItem);
                p.locked=false;
            }
        }
    }
}