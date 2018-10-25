using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public static class InputManager {

		public static float GetAxisForMovement(string axis, LocalPlayer player) {
			if (player.CanMove()) {
				return UnityEngine.Input.GetAxis(axis);
			} else {
				return 0;
			}
		}

		public static bool GetKey(KeyCode key, Player player){
			return UnityEngine.Input.GetKey(key);
		}
	}
}