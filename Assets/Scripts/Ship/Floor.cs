using System.Collections;
using System.Collections.Generic;
using Sink;
using UnityEngine;

public class Floor : MonoBehaviour {

	public List<Room> rooms;

	public OxygenLevel oxygen;

	public int number;

	public static int startOxygenDrop = 60;

	public LightController lights;

	public bool lightsAlertPermanent;

	/// <summary>
	/// loses 1 oxygen every n seconds
	/// </summary>
	public float oxLossRate = 1;

	public void Awake() {
		this.InvokeRepeatDelayed(LoseOxygen, oxLossRate, startOxygenDrop);
	}

	public void Start() {
		GameTimer.TimeLeftAlert += AlertLightsAtFiveMinutes;
	}

	public void LoseOxygen() {
		if (!GameTimer.paused) { oxygen.Adjust(-1); }
		AdjustLightsToOxygen();
	}

	public void AdjustLightsToOxygen() {
		if (oxygen.curOx <= 0) {
			lights.ChangeLightsToAlarm();
		} else if (!lightsAlertPermanent) {
			lights.ChangeLightsToNormal();
		}
	}

	public void AlertLightsAtFiveMinutes(int minutes, int seconds) {
		if (minutes == 4 && seconds == 59) {
			lights.ChangeLightsToAlarm();
			lightsAlertPermanent = true;
		}
	}

}