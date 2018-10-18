using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
namespace Sink {
	public class Item {

		public string Name;

		public Item()
		{}
		public Item(string name) {
			Name = name;
		}

		public Item(Item x)
		{
			Name = x.Name;
		}

	}
}