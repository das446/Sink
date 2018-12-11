using System.Collections;
using System.Collections.Generic;
using Sink;
using UnityEngine;

public class Floor : MonoBehaviour {

	public List<Room> rooms;

	public OxygenLevel oxygen;

	public int number;

	public static int startOxygenDrop = 60;

	/// <summary>
	/// loses 1 oxygen every n seconds
	/// </summary>
	public float oxLossRate = 1;

	public void Awake() {
		this.InvokeRepeatDelayed(LoseOxygen, oxLossRate, startOxygenDrop);
	}

	public void LoseOxygen() {
		if (!GameTimer.paused) { oxygen.Adjust(-1); }
	}
}