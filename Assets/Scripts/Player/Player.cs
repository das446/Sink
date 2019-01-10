using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sink {
	public class Player : MonoBehaviourPun, IPunObservable {

		public static List<Player> players = new List<Player>();

		public Room curRoom;
		public Floor curFloor;
		public int money;

		public Item item;

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

		public GameObject cam;

		public PlayerAnimator animator;

		public GameObject[] playerModels;

		public GameObject model;

		public static event Action<Player, Item> ItemChange;

		void Start() {
			DontDestroyOnLoad(gameObject);
			Debug.Log("SetupNetworking");
			SetupNetworking();
		}

		protected void Initialize() {
			curRoom = GameObject.Find(StartRoom).GetComponent<Room>(); //TODO: Don't use find
			curFloor = GameObject.Find("BottomFloor").GetComponent<Floor>(); //TODO: Don't use find
			curRoom.Enter(this);
			players.Add(this);
			ChangeModel(playerModels.RandomItem());
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

		public void GetItem(Item item) {
			this.item = item;
			ItemChange(this, item);
		}

		public void DropItem() {
			item = null;
			ItemChange(this, item);
		}

		public void UseItem(Item i) {
			if (item == i) {
				item.Use(this);
				item = null;
				ItemChange(this, item);
			}
		}

		public virtual void Lose() {

		}

		public virtual void SetupNetworking() {
			if (photonView.IsMine) {
				LocalPlayer player = gameObject.GetComponent<LocalPlayer>();
				player.enabled = true;
				gameObject.GetComponent<PlayerMovement>().enabled = true;
				cam.SetActive(true);
				gameObject.transform.GetChild(1).gameObject.SetActive(false);
				enabled = false;
				player.Initialize();

			} else {
				Destroy(GetComponent<LocalPlayer>());
				Initialize();
				
			}
		}

		public void Win() {
			Win(role);
		}

		public void Win(Role r) {
			gameOver = true;
			string playerRole = RoleToInitial(r);
			Win(playerRole);
		}

		public void Win(string r) {
			this.photonView.RPC("SetWinner", RpcTarget.All, r);
			PhotonNetwork.LoadLevel("EndScreen");
		}

		public static void WinGame(Role r) {
			LocalPlayer.singleton.Win(r);
		}

		public static void EveryoneLoses() {
			LocalPlayer.singleton.Win("L");
		}

		[PunRPC]
		public void SetWinner(string role) {
			PlayerPrefs.SetString("WinnerS", role);
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

		public virtual void ChangeRole(Role r) {
			role = r;
		}

		public void ChangeName(string n) {
			name = n;
			if (nameText != null) {
				nameText.text = n;
			}
		}

		public void ChangeModel(GameObject m) {
			if (animator == null || m == null) { return; }
			GameObject newModel = Instantiate(m, model.transform);
			animator.animator = newModel.GetComponent<Animator>();
			animator.animator.runtimeAnimatorController = animator.baseController;
		}

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		}
	}
}