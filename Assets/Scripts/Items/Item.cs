using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
namespace Sink {

	[CreateAssetMenu]
	public class Item : ScriptableObject {

<<<<<<< HEAD
		public Item()
		{}
		public Item(string name) {
			Name = name;
		}
=======
		public Image uiImage;

		public virtual void Use(Player User){
>>>>>>> 361d3bcaa66675173ffc18de313226b9a2b17da9

		public Item(Item x)
		{
			Name = x.Name;
		}

<<<<<<< HEAD
=======
		
>>>>>>> 361d3bcaa66675173ffc18de313226b9a2b17da9
	}
}