using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipEvent : MonoBehaviour {

	public float activationTime;

	public abstract void Activate();

}
