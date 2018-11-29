using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sink{

public class WarpFloor : MonoBehaviour {

	public Transform warpRecovery;
	
    
    public Room engine; // Used to set the player's room info to the zone the warp destination is

    public Floor bottomFloor; // Ignore any warnings you may see about not finding the class in the current namespace.
	
	// Update is called once per frame
	private void OnTriggerEnter(Collider other)
    {
        if (true &&  other.gameObject.GetComponent<LocalPlayer>() != null)
        {
           other.gameObject.GetComponent<LocalPlayer>().curRoom = engine;
           other.gameObject.GetComponent<LocalPlayer>().curFloor = bottomFloor;
			other.gameObject.transform.position = warpRecovery.transform.position;
            
        }
    }
    
}

}