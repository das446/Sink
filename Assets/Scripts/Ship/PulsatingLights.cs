using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

	public class PulsatingLights : MonoBehaviour {

		public Color startColor;
		public Color endColor;
		public Color curColor;
		public Color targetColor;
		public float speed;
		public Light[] lights;

		public void Update() {
			//if (curColor == endColor) { targetColor = startColor; } else if (curColor == startColor) { targetColor = endColor; }

			curColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1) * speed);
			foreach (Light light in lights) {

				light.color = curColor;
			}

		}

	}
}