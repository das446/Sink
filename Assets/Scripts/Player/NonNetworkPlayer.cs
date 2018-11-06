using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	/// <summary>
	/// This class is used so that you can test the main scene directly from the editor without having to first go to the lobby
	/// </summary>
	public class NonNetworkPlayer : LocalPlayer {

		// Use this for initialization
		protected override void Start() {
			if (Application.isEditor) {
				Destroy(gameObject);
			} else {
				inventory = new Inventory();
				curRoom = GameObject.Find("Room1").GetComponent<Room>(); //TODO: Don't use find
				curRoom.Enter(this);
			}
		}

		protected override void OnEnable(){

		}

		public override void SetupNetworking(){

		}

	}
}