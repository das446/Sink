using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

	public class LightController : MonoBehaviour {

		public Color startColor;
		public Color endColor;
		public Color curColor;
		public Color targetColor;
		public float speed;
		public Light[] lights;

		void Start() {
			startColor = lights[0].color;
			curColor = startColor;
		}

		void ChangeLightColor(Color c) {

			if (curColor == c) { return; }

			foreach (Light light in lights) {
				light.color = c;
			}
			curColor = c;

		}

		public void ChangeLightsToNormal() {
			ChangeLightColor(startColor);
		}

		public void ChangeLightsToAlarm() {
			ChangeLightColor(endColor);
		}

	}
}