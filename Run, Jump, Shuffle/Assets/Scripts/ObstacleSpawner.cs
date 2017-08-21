using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleSpawner : NetworkBehaviour {
	public int spawnTimer = 60;
	public float upOffset = 0.25f;
	public float downOffset = -0.05f;
	public GameObject obstacle;


	private int timer = 0;

	// Update is called once per frame
	void Start(){
		Random.InitState (0);
	}

	void Update () {
		if (!isLocalPlayer)
			return;
		if (timer++ >= spawnTimer) {
			timer = 0;
			spawn ();
		}
	}

	void spawn(){
		float yOffset = (Random.value > 0.5f ? downOffset : upOffset);
		float doubledYoffset = (Random.value > 0.5f ? upOffset : downOffset);
		if (GetComponent<PlayerController>().doubled) {
			GameObject doubledObst = Instantiate (obstacle, new Vector3 (GetComponentInParent<Transform> ().position.x + 4 + 1, doubledYoffset, -1), Quaternion.identity);
			doubledObst.GetComponent<ObstacleController> ().playerController = GetComponent<PlayerController> ();
			NetworkServer.Spawn (doubledObst);
		}
		GameObject obst = Instantiate (obstacle, new Vector3 (GetComponentInParent<Transform> ().position.x + 4, yOffset, -1), Quaternion.identity);
		obst.GetComponent<ObstacleController> ().playerController = GetComponent<PlayerController> ();
		NetworkServer.Spawn (obst);
	}
}
