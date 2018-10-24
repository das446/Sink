using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class OxygenController : Interactable {

		public Room room;
		public Item refItem;

		public override void DoAction(Player p) {
			
			// When multiple types of items get introduced,
			// each 'reference' item will given the name of the,
			// item it uses to be repaired which it will search for and remove and instance of.
			int size = p.inventory.items.Count;
			
			if ( size >= 1)
			{
				p.inventory.SpendItem(refItem);
				room.oxygen.setToMax();
			}

		}

        
    }
}