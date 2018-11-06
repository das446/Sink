using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	[CreateAssetMenu]
	public class Item : ScriptableObject {

		public Sprite uiImage;

		public GameObject model;

		public Vector3 scale = Vector3.one;

		public string description;

		public virtual void Use(Player User){

		}
		
	}
}