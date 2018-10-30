using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//Attach to item object, for iteractivity
namespace Sink {
	public class ItemInteractable : Interactable {

		[SyncVar]
		public string itemName;

		public Item item;

		public MeshFilter model;

		public GameObject child;

		void Start() {
			Initialize(itemName, transform.position);
		}

		public void Initialize(Item i, Vector3 pos) {

			item = i;

			model.mesh = item.model;
			transform.position = pos;
			transform.localScale = i.scale;

		}

		public void Initialize(string i, Vector3 pos) {

			itemName = i;
			item = ItemFromString(i);
			Initialize(item, pos);

		}

		public override void Interact(LocalPlayer p) {
			NetworkController.singleton.CmdInteract(gameObject, p.gameObject);
		}

		public void PickUp(Player p) {
			Debug.Log(item);
			Debug.Log(p);
			p.inventory.GetItem(item, 1);
			NetworkServer.Destroy(gameObject);
		}

		public override void DoAction(Player p) {

			if (p == null) {
				Debug.LogError("Player doing action is null");
				return;
			} else if (p.inventory == null) {
				p.inventory = new Inventory();
			}
			PickUp(p);

			gameObject.SetActive(false);
			//NetworkServer.Destroy(gameObject);

		}

		public Item ItemFromString(string n) {
			if (ItemSpawner.singleton == null) {
				ItemSpawner.singleton = GameObject.FindObjectOfType<ItemSpawner>();
				if (ItemSpawner.singleton == null) {
					Debug.LogWarning("No ItemSpawner in scene");
				}
			}
			return ItemSpawner.singleton.ItemFromString(n);
		}
	}

}