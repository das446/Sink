using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink
{
    public class MouseParamMenu : MonoBehaviour, IMenu
    {
		public void Close(LocalPlayer p) {
			gameObject.SetActive(false);
		}

		public void Open(LocalPlayer p) {
			gameObject.SetActive(true);
		}
		

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}