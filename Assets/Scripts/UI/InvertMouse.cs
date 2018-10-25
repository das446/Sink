using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class InvertMouse : MonoBehaviour
{
    public LocalPlayer player;
    
    public void InvertXAxis(bool yes)
    {
        //ml.XSensitivity*=-1;
    }

    public void InvertYAxis(bool yes)
    {
        //ml.YSensitivity*=1;
    }

    public void Test(bool test){

    }

    void Start(){

    }
    void Update(){
        
    }

    
}


/*
public class Invert : MonoBehaviour 
{

	public bool isXInverted;
	public bool isYInverted;
	public float rotationX = 0f;
	public float rotationY = 0f;
	public float minimumX = -90f;
	public float maximumX = 90f;
	public float minimumY = -90f;
	public float maximumY = 90f;
	public float YSensitivity = 2f;
	public float XSensitivity = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InvertedAxes()
	{

	}
*/


