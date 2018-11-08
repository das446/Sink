using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class ItemSpawner : NetworkBehaviour {

		public List<Item> possibleItems;
		public List<Transform> spawnerLocations; // Gathered from child objects

		public List<Room> rooms;

		public int amnt;

		public ItemInteractable baseItem;

		//public List<MeshFilter> possibleProps;

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
				SpawnItemInEachRoom();
			}
		}

		/// <summary>
		/// Itemspawner only without need for i command and using new Searchable items
		/// </summary>
		public void SpawnRandomItems(int amount) {
			List<Transform> alreadySpawned = new List<Transform>();
			if (amount <= spawnerLocations.Count) {
				for (int i = 0; i < amount; i++) {
					Item newItem = possibleItems[i];
					Transform t = rooms.RandomItem().possibleSpawnLocations.RandomItem();
					Vector3 v = t.position;
					CmdSpawnItem(newItem.name, v);
					alreadySpawned.Add(t); // prevents multiple items from being spawned in the same place.

				}
			}
		}

		public void SpawnItemInEachRoom() {
			foreach (Room room in rooms) {
				if(room==null){return;}
				Item newItem = possibleItems.RandomItem();
				if(room.possibleSpawnLocations==null){continue;}
				Transform t = room.possibleSpawnLocations.RandomItem();
				if(t==null){return;}
				Vector3 v = t.position;
				Debug.Log(v);
				CmdSpawnItem(newItem.name,v);

			}
		}

		/// <summary>
		/// TODO:
		/// </summary>
		/// <param name="amnt"></param>
		/// <returns></returns>
		List<Transform> GetRandomSpawnPoints(int amnt) {
			return null;
		}

		[Command]
		public void CmdSpawnItem(string itemName, Vector3 pos) {

			Item item = ItemFromString(itemName);
			ItemInteractable i = Instantiate(baseItem, pos, Quaternion.identity);
			i.itemName = item.name;
			//i.Initialize(itemName, pos);
			NetworkServer.Spawn(i.gameObject);
			//i.Initialize(item, pos);

		}

		public Item ItemFromString(string n) {
			return possibleItems.Where(i => i.name == n).First();
		}

	}
}