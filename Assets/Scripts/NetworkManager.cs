﻿using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	public GameObject loadcam;
	
	public Transform pos1;
	public Transform pos2;

	public bool offlineMode = false;
	bool connecting = false;

	// Use this for initialization
	void Start () {
		PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Guy");;
	}
	
	void OnDestroy() {
		PlayerPrefs.SetString("Username", PhotonNetwork.player.name);
	}

	void Connect() {
		PhotonNetwork.ConnectUsingSettings( "dkbox4" );
	}
	
	void OnGUI() {
		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );
		
		if (PhotonNetwork.connected == false && connecting == false) {
						GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
						GUILayout.BeginHorizontal ();
						GUILayout.FlexibleSpace ();
						GUILayout.BeginVertical ();
						GUILayout.FlexibleSpace ();
			
						GUILayout.BeginHorizontal ();
						GUILayout.Label ("Username: ");
						PhotonNetwork.player.name = GUILayout.TextField (PhotonNetwork.player.name);
						GUILayout.EndHorizontal ();
			
						/*
			if( GUILayout.Button("Single Player") ) {
				connecting = true;
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby();
			}
			*/
			
						if (GUILayout.Button ("Multi Player")) {
								connecting = true;
								Connect ();
						}
						GUILayout.FlexibleSpace ();
						GUILayout.EndVertical ();
						GUILayout.FlexibleSpace ();
						GUILayout.EndHorizontal ();
						GUILayout.EndArea ();
				}
	}
	
	void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null );
	}
	
	void OnJoinedRoom() {
		Debug.Log ("OnJoinedRoom");
		
		connecting = false;
		SpawnMyPlayer();
	}
	
	void SpawnMyPlayer() {
		Debug.Log("Spawning player: " + PhotonNetwork.player.name);

		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Alino", PhotonNetwork.playerList.Length > 1? pos1.localPosition : pos2.localPosition ,PhotonNetwork.playerList.Length > 1? pos1.localRotation : pos2.localRotation,0);
		loadcam.SetActive(false);
		myPlayerGO.transform.FindChild("OVRCameraController").gameObject.SetActive(true);
		//myPlayerGO.transform.GetComponent("PLayerMvt").enabled = true;
	}
}
