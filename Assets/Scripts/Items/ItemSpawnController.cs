using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


namespace Sink{
public class Item_Spawn_Controller : NetworkBehaviour {

	// Attach to host 
	// To prevent sever from spawning mutliple objects in the same space

	
		List<ItemInteractable> Item_props;

		public static ItemSpawner singleton; //use editor to set this 

		void  Start()
		{
			singleton.Host_Called(Item_props); // calls the singleton to spawn objects when host finishes loading in
			// Attempt to reuse already prexisting itemspawner script, now with new function.
		}
}
}