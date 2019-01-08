using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class ItemSpawner : NetworkBehaviour {

		public List<Item> possibleItems;
		//public List<Transform> spawnerLocations; // Gathered from child objects

		//public List<Room> rooms;

		//public int amnt;

		public ItemInteractable baseItem;

		//public List<MeshFilter> possibleProps;

		public static ItemSpawner singleton;

		public Item item_Battery;
		public Item item_Gear;
		//public Item item_Third;

		public List<Transform> battery_Locations;
		public List<Transform> gear_Locations;
		//public List <Transform> third_Locations;

		void Awake() {
			if (singleton == null) {
				singleton = this;
			} else {
				Destroy(gameObject);
			}
		}

		void Update() {
			
			/*   Manual method removed, kept for reference
			if (Input.GetKeyDown(KeyCode.I) && isServer) {
				Debug.Log("Spawning items");
				PlaceItems();
			}
			//*/


		}

		public void PlaceItems() {
			for (int i = 0; i < battery_Locations.Count; i++) {
				CmdSpawnItem("Battery_NoModel", battery_Locations[i].transform.position);
			}
			for (int i = 0; i < gear_Locations.Count; i++) {
				CmdSpawnItem("Gear_NoModel", gear_Locations[i].transform.position);
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
			i.model = null;
			NetworkServer.Spawn(i.gameObject);

		}

		public Item ItemFromString(string n) {
			return possibleItems.Where(i => i.name == n).First();
		}

	}
}