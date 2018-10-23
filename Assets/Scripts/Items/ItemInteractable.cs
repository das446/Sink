using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attach to item object, for iteractivity
namespace Sink {
	public class ItemInteractable : Interactable {

		public Item item;

		public MeshFilter model;

		

		public void Initialize(Item i, Vector3 pos ) {
			item = i;
			model.mesh = item.model;
			
		}

		public override void DoAction(Player p) {

			p.inventory.GetItem(item, 1);
			
			Destroy(gameObject);

		}
	}

}