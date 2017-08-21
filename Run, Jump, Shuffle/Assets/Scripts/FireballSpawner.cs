using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireballSpawner : NetworkBehaviour {
	public GameObject fireball;

	public void spawn(){
		GameObject fb = Instantiate (fireball, new Vector3 (GetComponentInParent<Transform> ().position.x + 4, 2.9f, -1), Quaternion.identity);
		NetworkServer.Spawn (fb);
	}
}
