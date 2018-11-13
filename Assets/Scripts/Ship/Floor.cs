using System.Collections;
using System.Collections.Generic;
using Sink;
using UnityEngine;

public class Floor : MonoBehaviour {

	public List<Room> rooms;

	public OxygenLevel oxygen;
	public ElecPower power;
	public Temperature temperature;

	public int number;

	/// <summary>
	/// loses 1 oxygen every n seconds
	/// </summary>
	public float OxLossRate = 1;

	public void Awake() {
		temperature = new Temperature();
		this.InvokeRepeat(LoseOxygen, OxLossRate);
	}

	public void LoseOxygen() {
		oxygen.Adjust(-1);
	}

}