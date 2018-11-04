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

		public static ItemSpawner singleton;

		void Awake() {
			if (singleton == null) {
				singleton = this;
			} else {
				Destroy(gameObject);
			}
		}

		void Update() {
			if (Input.GetKeyDown(KeyCode.I) && isServer) {
				List<Transform> alreadySpawned = new List<Transform>();
				if (AmntItemsSpawnAtStart <= spawnerLocations.Count) {
					for (int i = 0; i < AmntItemsSpawnAtStart; i++) {
						Item newItem = possibleItems[i];
						Transform t = rooms.RandomItem().possibleSpawnLocations.RandomItem();
						Vector3 v = t.position;
						CmdSpawnItem(newItem.name, v);
						alreadySpawned.Add(t); // prevents multiple items from being spawned in the same place.

					}
				}
			}

		}

		/// <summary>
		/// Itemspawner only without need for i command and using new Searchable items
		/// </summary>
		/// <param name="Item_props"></param>
		public void HostCalled(List<ItemSearch> Item_props) {
			List<Transform> alreadySpawned = new List<Transform>();
			if (AmntItemsSpawnAtStart <= spawnerLocations.Count) {
				for (int i = 0; i < AmntItemsSpawnAtStart; i++) {
					ItemSearch newItem = Item_props[i];
					Transform t = rooms.RandomItem().possibleSpawnLocations.RandomItem();
					Vector3 v = t.position;
					CmdSpawnItem(newItem.name, v);
					alreadySpawned.Add(t); // prevents multiple items from being spawned in the same place.

				}
			}
		}

		[Command]
		public void CmdSpawnItem(string itemName, Vector3 pos) {

			Item item = ItemFromString(itemName);
			ItemInteractable i = Instantiate(baseItem, pos, Quaternion.identity);
			i.itemName = item.name;
			i.Initialize(itemName, pos);
			NetworkServer.Spawn(i.gameObject);
			i.Initialize(item, pos);

		}

		public Item ItemFromString(string n) {
			return possibleItems.Where(i => i.name == n).First();
		}

	}
}