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
	public float oxLossRate = 1;

	/// <summary>
	/// loses 1 power every n seconds
	/// </summary>
	public float powerLossRate = 1;

	/// <summary>
	/// loses 1 power every n seconds
	/// </summary>
	public float temperatureLossRate = 1;

	public void Awake() {
		temperature = new Temperature();
		this.InvokeRepeat(LoseOxygen, oxLossRate);
		this.InvokeRepeat(LoseTemperature, temperatureLossRate);
		this.InvokeRepeat(LosePower, powerLossRate);
	}

	public void LoseOxygen() {
		oxygen.Adjust(-1);
	}

	public void LoseTemperature() {
		temperature.Adjust(-1);
	}

	public void LosePower() {
		power.Adjust(-1);
	}

}