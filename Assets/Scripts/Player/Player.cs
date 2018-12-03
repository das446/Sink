using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sink {
	public class Player : NetworkBehaviour {

		public static List<Player> players = new List<Player>();

		public Room curRoom;
		public Floor curFloor;
		public int money;

		public Inventory inventory;

		public NetworkController networkController;
		public bool local = false;

		public string StartRoom;

		public NetworkMovement networkMovement;

		public bool locked = false;

		public enum Role { Crew, Saboteur }

		public Role role = Role.Crew;

		[SerializeField]
		private LocalPlayer player;

		public float WalkThroughDoorSpeed = 50;
		public float ClimbLadderSpeed = 50;

		public new Collider collider;

		public CharacterController cc;

		public string playerName;

		public TMPro.TMP_Text nameText;

		public bool gameOver = false;

		public bool searching;

		protected virtual void Start() {
			if (SceneManager.GetActiveScene().name == "EndScreen") { return; }
			if (playerName == "") {
				playerName = "Player" + GetComponent<NetworkIdentity>().netId;
			}
			inventory = new Inventory();
			curRoom = GameObject.Find(StartRoom).GetComponent<Room>(); //TODO: Don't use find
			curFloor = GameObject.Find("BottomFloor").GetComponent<Floor>(); //TODO: Don't use find
			curRoom.Enter(this);
			players.Add(this);

		}

		public void GetMoney(int amnt) {
			money += amnt;
		}

		public virtual void MoveToRoom(Room room) {
			curRoom.Exit(this);
			room.Enter(this);
			curRoom = room;
		}

		public virtual void MoveToFloor(Floor floor) {
			curFloor = floor;
		}

		public virtual IEnumerator WalkThroughDoor(Door door, Room room) {
			MoveToRoom(room);
			yield return null;
		}

		public virtual IEnumerator ClimbLadder(Ladder ladder, Room room, Floor floor) {
			MoveToRoom(room);
			MoveToFloor(floor);
			yield return null;

		}

		public virtual void RecieveMove(string s) {

		}

		public virtual void Setup() {

		}

		public override void OnStartAuthority() {
			SetupNetworking();
		}

		public void GetItem(Item item) {
			inventory.GetItem(item);
		}

		public virtual void Lose() {

		}

		public virtual void SetupNetworking() {
			if (hasAuthority) {
				LocalPlayer player = gameObject.GetComponent<LocalPlayer>();
				player.enabled = true;
				gameObject.GetComponent<PlayerMovement>().enabled = true;
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				Destroy(GetComponent<NetworkMovement>());
				gameObject.transform.GetChild(1).gameObject.SetActive(false);
				enabled = false;

			} else {
				Destroy(GetComponent<LocalPlayer>());

			}
		}

		public void Win() {
			gameOver = true;
			string playerRole = RoleToInitial(role);
			NetworkController.singleton.CmdSendWinnerOverNetwork(playerRole);
			NetworkManager.singleton.ServerChangeScene("EndScreen");
		}

		public static void Win(Role r) {
			string playerRole = RoleToInitial(r);
			NetworkController.singleton.CmdSendWinnerOverNetwork(playerRole);
			NetworkManager.singleton.ServerChangeScene("EndScreen");
		}

		public string RoleToInitial() {
			return role == Role.Crew ? "C" : "S";
		}

		public Role Enemy() {
			return role == Role.Crew ? Role.Saboteur : Role.Crew;
		}

		public static string RoleToInitial(Role r) {
			return r == Role.Crew ? "C" : "S";
		}

		public void UpdateTargetPos(Vector3 p, float rotY) {
			if (hasAuthority || networkMovement == null) { return; }
			networkMovement.target = p;
			networkMovement.rotY = rotY;
		}

		public void ChangeRole(Role r) {
			NetworkController.singleton.CmdChangePlayerRole(gameObject, r);
		}

		public virtual void OnChangeRole(Role r) {
			role = r;
		}
		
		public void ChangeName(string n){
			NetworkController.singleton.CmdChangePlayerName(gameObject,n);
		}

		public void OnChangeName(string n) {
			name = n;
			if (nameText != null) {
				nameText.text = n;
			}
		}

	}
}