using System.Collections;
using System.Collections.Generic;
using Sink.Audio;
using UnityEngine;
using UnityEngine.Networking;
//Attach to item object, for iteractivity
namespace Sink {
	public class ItemInteractable : Interactable {

		[SyncVar]
		public string itemName;

		public Item item;

		public MeshFilter model;

		public GameObject child;

		public static List<ItemInteractable> itemsInScene = new List<ItemInteractable>();

		/// After consideration it made more sense to just add the alterations to iteminteractable than to go ahead and make a new class
		public ProgressBar bar;
		public TMPro.TMP_Text text; // not entirely sure if this is required to print out time remaining display
		public float search_time;
		/// All functions intially in ItemSearch are now in ItemInteractable, as one was intended to replace the other anyway

		void Start() {

			Initialize(itemName, transform.position);

			bar.text = text;
			bar.Finish += OnBarFinish;
		}

		public void Initialize(Item i, Vector3 pos) {

			item = i;

			GameObject model = Instantiate(i.model, Vector3.zero, transform.rotation, transform);
			model.transform.localPosition = Vector3.zero;
			transform.position = pos;
			model.transform.localScale = i.scale;

		}

		public void Initialize(string i, Vector3 pos) {

			itemName = i;
			item = ItemFromString(i);
			Initialize(item, pos);

		}

		public override void Interact(LocalPlayer p) {
			NetworkController.singleton.CmdInteract(gameObject, p.gameObject);
		}

		public void PickUp(Player p) {
			p.item = item;
			gameObject.SetActive(false);
			NetworkServer.Destroy(gameObject);
		}
		/// 
		public void OnBarFinish(Player p) {
			PickUp(p);
		}
		///

		public override void DoAction(Player p) {

			if (p == null) {
				Debug.LogError("Player doing action is null");
				return;
			}
			bar.timeToComplete = search_time;
			bar.Activate(p);

			//gameObject.SetActive(false);
			//NetworkServer.Destroy(gameObject);

		}

		public Item ItemFromString(string n) {
			if (ItemSpawner.singleton == null) {
				ItemSpawner.singleton = GameObject.FindObjectOfType<ItemSpawner>();
				if (ItemSpawner.singleton == null) {
					Debug.LogWarning("No ItemSpawner in scene");
				}
			}
			return ItemSpawner.singleton.ItemFromString(n);
		}
	}

}