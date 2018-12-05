using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public class PlayerAnimator : MonoBehaviour {

		public Player player;
		[SerializeField] Animator animator;

		public void Grab() {
			animator.SetTrigger("Grab");
		}

		public void SetSpeed(float speed){
			animator.SetFloat("Speed",speed);
		}

	}
}