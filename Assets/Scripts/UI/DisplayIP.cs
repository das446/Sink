using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class DisplayIP : MonoBehaviour {

	public Text text;

	void Start() {
		text.text = "Your IP: " + GetLocalIPAddress();
	}

	public string GetLocalIPAddress() {
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList) {
			if (ip.AddressFamily == AddressFamily.InterNetwork) {
				return ip.ToString();
			}
		}
		return "No network adapters with an IPv4 address in the system!";
	}
}