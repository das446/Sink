using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class PlayerAnimator : MonoBehaviour {

		public Player player;
		public Animator animator;
		public RuntimeAnimatorController baseController;

		public void Grab() {
			animator?.SetTrigger("Grab");
		}

		public void SetSpeed(float speed) {
			if (animator != null) {
				animator.SetFloat("Speed", speed);
			}
		}

	}
}