using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class DoorSpeed : MonoBehaviour {

	//Placeholder code to change door opening and closing speed parameters

	public Animator DoorAnimator;
	public float doorSpeed = 1f;


	// Use this for initialization
	void Start () 
	{
		DoorAnimator = GetComponent<Animator>();	
	}
	
	void OnTriggerEnter()
	{
		//DoorAnimator.SetTrigger("open");
		DoorAnimator.Play("Door open");
		DoorAnimator.SetFloat("speed", doorSpeed);
	}

	void OnTriggerExit()
	{
		//DoorAnimator.SetTrigger("close");
		DoorAnimator.Play("Door close");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
