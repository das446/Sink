using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sink {
	public class MenuController : MonoBehaviour {

		public Player player;

		int cur = 0;

		public List<GameObject> Menus;

		public List<IMenu> menus;
		public GameObject lArrow, rArrow;

		public void Start() {
			menus = Menus.Select(x => x.GetComponent<IMenu>()).ToList();

		}

		public void Open() {
			menus[cur].Open();
			lArrow.SetActive(true);
			rArrow.SetActive(true);
		}

		public void Next() {
			menus[cur].Close();
			cur++;
			if (cur == menus.Count) {
				cur = 0;
			}
			menus[cur].Open();

		}

		public void Prev() {
			menus[cur].Close();
			cur--;
			if (cur == -1) {
				cur = menus.Count-1;
			}
			menus[cur].Open();

		}

		public void Close() {
			menus[cur].Close();
			lArrow.SetActive(false);
			rArrow.SetActive(false);

		}

	}

	public interface IMenu {
		void Close();
		void Open();
	}
}