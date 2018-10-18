using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sink {
	public class MenuController : MonoBehaviour {

		public LocalPlayer player;

		int cur = 0;

		public List<GameObject> Menus;

		public List<IMenu> menus;
		public GameObject lArrow, rArrow;

		public void Start() {
			menus = Menus.Select(x => x.GetComponent<IMenu>()).ToList();

		}

		public void Open() {
			menus[cur].Open(player);
			lArrow.SetActive(true);
			rArrow.SetActive(true);
		}

		public void Next() {
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

		}

	}

	public interface IMenu {
		void Close(LocalPlayer p);
		void Open(LocalPlayer p);
	}
}