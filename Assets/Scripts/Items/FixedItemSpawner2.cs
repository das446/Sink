using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Sink{
public class FixedItemSpawner2 : NetworkBehaviour {

	public ItemInteractable baseItem;
	
	public Item item_Battery;
	public Item item_Gear;
	//public Item item_Third;

	public List <Transform> battery_Locations;
	public List <Transform> gear_Locations;
	//public List <Transform> third_Locations;

	public static FixedItemSpawner2 singleton;
	public void PlaceItems()
	{
		for (int i = 0; i < battery_Locations.Count;i++ )
		{
			CmdSpawnItem(item_Battery,battery_Locations[i].transform.position,"Battery");
		}
		for (int i = 0; i < gear_Locations.Count;i++ )
		{
			CmdSpawnItem(item_Battery,gear_Locations[i].transform.position,"Gear");
		}
	/* 	for (int i = 0; i < third_Locations.Count;i++ )
		{
			CmdSpawnItem(item_Battery,third_Locations[i].transform.position);
		}
	*/
	}

	public void CmdSpawnItem(Item item, Vector3 pos,string id) {

			
			ItemInteractable i = Instantiate(baseItem, pos, Quaternion.identity);
			i.name = id;
			MeshRenderer mesh_ = i.GetComponent<MeshRenderer>();
			mesh_.enabled = false;
			
			
			//i.Initialize(itemName, pos);
			NetworkServer.Spawn(i.gameObject);
			//i.Initialize(item, pos);

		}
		void Awake() {
			if (singleton == null) {
				singleton = this;
			} else {
				Destroy(gameObject);
			}
		}
		

}
}
