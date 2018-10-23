using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class ItemSpawner : NetworkBehaviour {

		public List<Item> possibleItems;
		public List<Transform> spawnerLocations; // Gathered from child objects

		public int UsedSpawnLocations; // Dictates how many items should spawn

		public ItemInteractable baseItem;

		void Start() {
			spawnerLocations = System.Array.FindAll(GetComponentsInChildren<Transform>(), child => child != this.transform).ToList();

			if (UsedSpawnLocations < spawnerLocations.Count) {
				for (int i = 0; i < UsedSpawnLocations; i++) {
					int randInt = Random.Range(0, spawnerLocations.Count - 1);
					Item newItem = (Item) ScriptableObject.CreateInstance("Item");
					newItem = possibleItems.ElementAt(0);
					SpawnItem(newItem, spawnerLocations.ElementAt(randInt));
					spawnerLocations.RemoveAt(randInt); // prevents multiple items from being spawned in the same place.

				}
			}

		}

		public void SpawnItem(Item item, Transform spawner) {
			Vector3 location = spawner.position;

			ItemInteractable i = Instantiate(baseItem, location, Quaternion.identity);
			i.Initialize(item, location);
			NetworkServer.Spawn(i.gameObject);

		}

	}
}