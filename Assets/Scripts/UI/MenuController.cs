using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sink {
	public class MenuController : MonoBehaviour {

		public LocalPlayer player;

		int cur = 0;

		public List<IMenu> menus;
		public GameObject lArrow, rArrow;

		public void Start() {
		}

		public void Open(LocalPlayer player) {
			gameObject.SetActive(true);
			this.player = player;
			Debug.Log(menus[cur]);
			Debug.Log(player);
			menus[cur].Open(this.player);
			lArrow.SetActive(true);
			rArrow.SetActive(true);
		}

		public void Open(LocalPlayer player, int index){
			cur = index;
			Open(player);
		}

		public void Next() {

			Debug.Log(cur);

			menus[cur].Close(player);
			
			cur++;
			if (cur == menus.Count) {
				cur = 0;
			}
			menus[cur].Open(player);

		}

		public void Prev() {
			menus[cur].Close(player);
			cur--;
			if (cur == -1) {
				cur = menus.Count - 1;
			}
			menus[cur].Open(player);

		}

		public void Close() {
			menus[cur].Close(player);
			lArrow.SetActive(false);
			rArrow.SetActive(false);
			gameObject.SetActive(false);

		}

	}
}