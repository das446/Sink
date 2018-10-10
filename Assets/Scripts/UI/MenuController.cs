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

		public void Start() {
			menus = Menus.Select(x => x.GetComponent<IMenu>()).ToList();
		}

		public void Open() {
			menus[cur].Open();
		}

		public void Next() {
			menus[cur].Close();
			cur = cur == menus.Count - 1 ? 0 : cur++;
			menus[cur].Open();

		}

		public void Prev() {
			menus[cur].Close();
			cur = cur == 0 ? menus.Count : cur--;
			menus[cur].Open();

		}

		public void Close() {
			menus[cur].Close();
		}

	}

	public interface IMenu {
		void Close();
		void Open();
	}
}