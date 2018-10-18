using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

    public class ItemObject : Interactable {

        public Item item;

        public override void DoAction(Player p) {
            p.GetItem(item);
            Destroy(gameObject);
        }
    }
}