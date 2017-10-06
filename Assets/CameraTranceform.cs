using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranceform : MonoBehaviour {

	public GameObject Player;
	GameObject mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
	}
	// Update is called once per frame
	void Update () {

		mainCamera.transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y + 2, Player.transform.position.z - 5);

	}
}
