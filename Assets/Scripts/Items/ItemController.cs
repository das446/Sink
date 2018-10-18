using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attach to item object, for iteractivity
namespace Sink {
public class ItemController : Interactable {

	//public Inventory inventory;
	public string iname;
	public Item item;

	private GameObject self;

	void Start()
	{
		self =  gameObject;
		item = new Item(iname);
	}
		

		protected override void DoAction(Player p) {
			//Debug.Log(p.inventory.items.Count);
			p.inventory.GetItem(item,1);
		    //Debug.Log(p.inventory.items.Count);
			Object.Destroy(self);
			
		}
}


}
