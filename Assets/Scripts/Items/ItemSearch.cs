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

	public Item item;
	public ProgressBar bar;

	public Mesh item_prop;

	public float searchTime;

	public GameObject model;

	private int amntLeft; // Amount of items stored in said object
	public int startAmnt;

	public bool beingSearched;
	public float respawn = 30;

	void Start() {
		bar.Finish += OnBarFinish;

		beingSearched = false;
		if (startAmnt <= 0) {
			startAmnt = 1;
		}
		amntLeft = startAmnt; // used for amount ref. when items are respawned.

	}

	public override void DoAction(Player p) {
		if (!beingSearched) {
			bar.timeToComplete = searchTime;
			bar.Activate(p);
			p.locked = true;
		}

	}

	public bool CanSearch() {
		return amntLeft > 0 && !beingSearched;
	}

	public void OnBarFinish(Player p) {
		p.GetItem(item);
		Destroy(model);
		bar.DisplayMessage("Got a " + item.name, "", 3);
		beingSearched = true;
		p.locked = false;
		this.DoAfterTime(() => {
			beingSearched = false;
		}, respawn);

	}

}