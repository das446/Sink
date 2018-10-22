using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {

	public class ItemSpawner : NetworkBehaviour {

		public List<Item> possibleItems;
		public List<Room> rooms;

		public ItemInteractable baseItem;

		void Start(){
			Item i = possibleItems.RandomItem();
			Room r = rooms.RandomItem();
			SpawnItem(i,r);
		}

		public void SpawnItem(Item item, Room room) {
			Vector3 location = room.possibleSpawnLocations.RandomItem();
			ItemInteractable i = Instantiate(baseItem,location,Quaternion.identity);
			i.Initialize(item,location);
			NetworkServer.Spawn(i.gameObject);


		}

	}
}