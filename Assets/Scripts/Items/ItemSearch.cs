using System.Collections;
using System.Collections.Generic;
using cakeslice;
using Sink;
using Sink.Audio;
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class ItemSearch : Interactable {

	public Item item;
	public ProgressBar bar;

	public Mesh item_prop;

	public float searchTime;

	public GameObject model;

	private int amntLeft; // Amount of items stored in object
	public int startAmnt;

	public bool beingSearched;
	public float respawn = 30;

	void Start() {
		bar.Finish += OnBarFinish;

		beingSearched = false;
		if (startAmnt <= 0) {
			startAmnt = 1;
		}
		amntLeft = startAmnt;
		if (outline == null) {
			outline = GetComponent<Outline>();
		}

	}

	public override void DoAction(Player p) {
		if (CanSearch(p)) {
			bar.timeToComplete = searchTime;
			bar.Activate(p);
			p.searching = true;
			p.animator.Grab();
			PlaySoundLocalOnly("Searching", p);
		}

	}

	public bool CanSearch(Player p) {
		return amntLeft > 0 && !beingSearched && p.item == null;
	}

	public void OnBarFinish(Player p) {
		p.GetItem(item);
		bar.DisplayMessage("Got a " + item.name, "", 3);
		beingSearched = true;
		p.searching = false;
		this.DoAfterTime(() => {
			beingSearched = false;
		}, respawn);

	}

}