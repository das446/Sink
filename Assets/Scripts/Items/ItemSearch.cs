using System.Collections;
using System.Collections.Generic;
using Sink;
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class ItemSearch : Interactable {

	// Select enviroment object (dresser / chest / etc )
	// Takes x minutes with timer
	// despawn said object and add item
	// inhert from interactable for doplayer function
	// 

	public Item item;
	public ProgressBar bar;
	public TMPro.TMP_Text text; // not entirely sure if this is required to print out time remaining display

	public Mesh item_prop;

	public float searchTime;

	public GameObject model;

	void Start() {
		if (model == null) { model = gameObject; }
		bar.text = text;
		bar.Finish += OnBarFinish;
	}

	public override void DoAction(Player p) {
		bar.timeToComplete = searchTime;
		bar.Activate(p);

	}

	public void OnBarFinish(Player p) {
		p.GetItem(item);
		Destroy(model);
	}

}