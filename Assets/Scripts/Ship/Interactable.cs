using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public abstract class Interactable : MonoBehaviour {

		public static Dictionary<string, Interactable> Interactables = new Dictionary<string, Interactable>();

		public string guid;

		public static bool networking = false;

		void Start() {
			guid = name;
			if (Interactables.ContainsKey(guid)) {
				Debug.LogError("Error, 2 interactables can't have the same name:" + guid);
			} else {
				Interactables.Add(guid, this);
			}
		}

		public virtual void Interact(Player p) {
			//TODO: have a switch case based on a settings file
			if (networking) {
				SendServerMessage(p);
			}
			else{
				DoAction(p);
			}
		}
		
		protected abstract void DoAction(Player p);
		protected virtual string SendServerMessage(Player p){
			string s = "I|"+guid+"|"+p.name;
			//TODO: have client write to server
			return s;
		}
		protected virtual void GetServerMessage(string[] s){
			Player p = null;//TODO: get from string with list of players
			DoAction(p);
		}

		/// <summary>
		/// Takes a server message and assigns it to the correct object to handle it
		/// </summary>
		/// <param name="s">format = I|GUID|Player|params...</param>
		public static void AssignServerMessage(string[] s) {
			string guid = s[1];
			Interactable i = Interactables[guid];
			i.GetServerMessage(s);
		}

	}
}