using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipEvent : ScriptableObject {

	public float activationTime;

	public abstract void Activate();

}
