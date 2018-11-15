using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink
{
public class SteamBehavior : MonoBehaviour
 {

	public GameObject smoke;
	public GameObject pipe;
	Room room;
	//bool isOn = true;
   //bool enter = true;
	
	// Use this for initialization
	void Start () 
	{
		smoke.gameObject.SetActive(false);
		//smoke.GetComponent<ParticleSystem>().enableEmission = false;
		//gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnTriggerEnter(Collider c)
	{
        if(c.GetComponent<Player>())
        {
            smoke.gameObject.SetActive(true);
        }
        /*
		if(c.tag == "Player")
		{
			smoke.Play();
			Debug.Log("You have activated the smoke");
		}
*?
        /*
		if(c.GetComponent<Collider>().tag == "Player")
		{
			c.gameObject.GetComponent<ParticleSystem>().Play();
		}
        */
		//smoke.GetComponent<ParticleSystem>().enableEmission = true;
		

		/*
		if(enter)
		{
			GetComponent<ParticleSystem>().Play();
			ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
			em.enabled = true;
			Debug.Log("Smoke will activate");
		}
		*/
	}
}
}
