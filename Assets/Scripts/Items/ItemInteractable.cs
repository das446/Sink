using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attach to item object, for iteractivity
namespace Sink {
	public class ItemInteractable : Interactable {

		public Item item;

		public MeshFilter model;

		public GameObject child;

		public void Initialize(Item i, Vector3 pos) {
			item = i;
			model.mesh = item.model;
			transform.position = pos;
			child.transform.localScale=i.scale;
			
		}

		public override void DoAction(Player p) {
			//Debug.Log(p.inventory.items.Count);

			p.inventory.GetItem(item, 1);
			//Debug.Log(p.inventory.items.Count);
			Destroy(gameObject);

		}
	}

}