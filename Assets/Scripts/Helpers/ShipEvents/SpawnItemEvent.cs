using System.Collections;
using System.Collections.Generic;
using Sink;
using UnityEngine;

public class SpawnItemEvent : ShipEvent {

	public Item item;

	public Transform pos;

	public override void Activate() {
		ItemSpawner.singleton.CmdSpawnItem(item.name, pos.position);
	}

}