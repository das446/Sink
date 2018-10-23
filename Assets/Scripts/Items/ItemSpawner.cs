using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class ItemSpawner : NetworkBehaviour {

		public List<Item> possibleItems;
		public List<Transform> spawnerLocations; // Gathered from child objects

		public List<Room> rooms; //later it should have a list of all the rooms, and each room will have spawn locations but for now it's fine

		public int AmntItemsSpawnAtStart;

		public ItemInteractable baseItem;

		void Update() {
			if (Input.GetKeyDown(KeyCode.I) && isServer) {

				spawnerLocations = System.Array.FindAll(GetComponentsInChildren<Transform>(), child => child != this.transform).ToList();

				if (AmntItemsSpawnAtStart <= spawnerLocations.Count) {
					for (int i = 0; i < AmntItemsSpawnAtStart; i++) {
						Item newItem = possibleItems.RandomItem();
						Transform t = spawnerLocations.RandomItem();
						Vector3 v = t.position;
						CmdSpawnItem(newItem.name, v);
						spawnerLocations.Remove(t); // prevents multiple items from being spawned in the same place.

					}
				}
			}

		}

		[Command]
		public void CmdSpawnItem(string itemName, Vector3 pos) {

			Item item = ItemFromString(itemName);
			ItemInteractable i = Instantiate(baseItem, pos, Quaternion.identity);
			i.Initialize(item, pos);
			Debug.Log("NetworkSpawn");
			NetworkServer.Spawn(i.gameObject);

		}

		public Item ItemFromString(string n){
			return possibleItems.Where(i => i.name == n).First();
		}

	}
}