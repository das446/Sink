using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class TextMeshFaceCamera : MonoBehaviour {

		void Update() {
			float y = Camera.main.transform.eulerAngles.y;
			Vector3 a = transform.eulerAngles;
			a.y = y;
			transform.eulerAngles = a;
		}
	}
}