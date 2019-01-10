using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice; //For Outline
using Sink.Audio;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


namespace Sink {

	public abstract class Interactable : MonoBehaviourPun, IHasOutline {

		public static Dictionary<string, Interactable> Interactables = new Dictionary<string, Interactable>();

		public string guid;

		public static bool networking = false;

		public Outline outline;
		public Outline GetOutline() {
			return outline;
		}
		public virtual bool Valid(Player p) {
			return true;
		}

		public virtual bool CanInteract(Player p) { return true; }

		[SerializeField] public RandomEvent re;

		/// <summary>
		/// This function only gets called localy, DoAction gets called by it
		/// </summary>
		/// <remarks>
		/// If you don't want it sent to the server immediately override it and call CmdInteract later 
		/// </remarks>
		public virtual void Interact(LocalPlayer p) {
			this.photonView.RPC("DoAction",RpcTarget.All,p);
		}

		public virtual void NetworkCancelInteract(LocalPlayer p) {
			this.photonView.RPC("CancelInteract",RpcTarget.All,p);
		}

		public virtual void SendMessage(Player p) {
			string message = p.name + " interacted with " + name;
		}

		/// <summary>
		/// This function gets called on the server, don't call it directly from other objects, use Interact instead
		/// </summary>
		[PunRPC]
		public abstract void DoAction(Player p);

		[PunRPC]
		public virtual void CancelInteract(LocalPlayer p) {

		}

		/// <summary>
		/// Only plays sound if user is local player
		/// </summary>
		/// <param name="sound"></param>
		public void PlaySoundLocalOnly(string sound, Player p) {
			if (p == LocalPlayer.singleton) {
				this.PlaySound(sound);
			}
		}

		protected void CheckRandomTrigger() {
			re?.CheckTrigger();
		}

		//The below functions are used to allow the user to start a game directly from the main scene to make testing easier
		void OnDisable() {
			if (Application.isEditor) {
				Invoke("EditorInit",1);
			}
		}

		void EditorInit(){
			gameObject.SetActive(true);
		}

		void Test(){

		}

		void TestRpc(){
			this.photonView.RPC2(Test);
		}

	}
}