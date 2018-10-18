using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sink {

	[CreateAssetMenu]
	public class Item : ScriptableObject {

		public Sprite uiImage;

		public Mesh model;

		public virtual void Use(Player User){

		}
		
	}
}