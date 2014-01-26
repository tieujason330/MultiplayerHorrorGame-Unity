using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;

	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect ();
	}

	void Connect()
	{
		PhotonNetwork.ConnectUsingSettings ("MultiFPS v001");

	}

	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby()
	{
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom()
	{
		Debug.Log ("OnJoinedRoom");

		SpawnMyPlayer ();
	}

	void SpawnMyPlayer()
	{
		if (spawnSpots == null) {
			Debug.LogError("Error");
			return;
				}

		SpawnSpot mySpawnSpot = spawnSpots[ Random.Range(0, spawnSpots.Length) ];
		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.SetActive(false);

		//for each individual player
		//((MonoBehaviour)myPlayerGO.GetComponent ("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent ("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent ("PlayerMovement")).enabled = true;
		//if (((MonoBehaviour)myPlayerGO.GetComponent ("PlayerMovement")).enabled == true)
						//Debug.Log ("PlayerMovement is TRUE");
		
		myPlayerGO.transform.FindChild ("Main Camera").gameObject.SetActive (true);
	}
}
