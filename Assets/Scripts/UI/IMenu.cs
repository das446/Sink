using System.Collections;
using System.Collections.Generic;
using Sink;
using UnityEngine;

public abstract class IMenu : MonoBehaviour {

	protected LocalPlayer player;

	public virtual void Close(LocalPlayer p) {
		gameObject.SetActive(false);
		player = p;
	}

	public virtual void Open(LocalPlayer p) {
		gameObject.SetActive(true);
	}
}