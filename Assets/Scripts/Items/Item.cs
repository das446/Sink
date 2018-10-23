using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	[CreateAssetMenu]
	public class Item : ScriptableObject {

		public Sprite uiImage;

		public Mesh model;

		public Vector3 scale;

		public virtual void Use(Player User){

		}
		
	}
}