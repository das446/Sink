using System.Collections;
using System.Collections.Generic;
using Sink;
using UnityEngine;

/// <summary>
/// Select enviroment object (dresser / chest / etc )
/// Takes x minutes with timer
/// despawn said object and add item
/// inhert from interactable for doplayer function
/// </summary>
[RequireComponent(typeof(ProgressBar))]
public class ItemSearch : Interactable {

	// 
	// 

	public Item item;
	public ProgressBar bar;
	public TMPro.TMP_Text text; // not entirely sure if this is required to print out time remaining display

	public Mesh item_prop;

	public float searchTime;

	public GameObject model;

	private int amntLeft; // Amount of items stored in said object
	public int startAmnt;

	public bool beingSearched;
	public float respawn;

	void Start() {
		bar.text = text;
		bar.Finish += OnBarFinish;
		respawn = 30;
		beingSearched = false;
		if ((startAmnt <= 0) || (startAmnt.Equals(null))) {
			startAmnt = 1;
		}
		amntLeft = startAmnt; // used for amount ref. when items are respawned.
	}

	public override void DoAction(Player p) {
		if (!beingSearched) {
			bar.timeToComplete = searchTime;
			bar.Activate(p);
		}

	}

	public bool CanSearch() {
		return amntLeft > 0 && !beingSearched;
	}

	public void OnBarFinish(Player p) {
		p.GetItem(item);
		Destroy(model);
		bar.text.text = "Got a " + item.name;
		beingSearched = true;
		this.DoAfterTime(() => {
			beingSearched = false;
		}, respawn);

	}

}