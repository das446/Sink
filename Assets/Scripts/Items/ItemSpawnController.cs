using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink {
	public class ItemSpawnController : NetworkBehaviour {

		// Attach to host 
		// To prevent sever from spawning mutliple objects in the same space

		public List<Item> itemProps;

		public float timeLeft;
		public float itemRespawnTime;

		public static ItemSpawnController singleton; //use editor to set this 

		void Start() {
			singleton = this;
			ItemSpawner.singleton.PlaceItems(); // calls the singleton to spawn objects when host finishes loading in
			// Attempt to reuse already prexisting itemspawner script, now with new function.

			timeLeft = itemRespawnTime;
		}
		void Update()
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0)
			{
				ItemSpawner.singleton.PlaceItems();
				timeLeft = itemRespawnTime;
			}
		} 

	}
}