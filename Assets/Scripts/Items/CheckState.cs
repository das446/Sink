using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : MonoBehaviour {

	public GameObject smoke;

	// Use this for initialization
	void Start () {
		Debug.Log("Active Self: " + smoke.activeSelf);
		Debug.Log("Active in hierarchy:" + smoke.activeInHierarchy);
		Debug.Log("Active Smoke");
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
