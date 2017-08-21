using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleController : NetworkBehaviour {
	public float speed = -2.25f;
	// Use this for initialization
	void Start(){
		GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, 0f);
	}
	void OnTriggerEnter2D(Collider2D collision){
		Destroy (this.gameObject);
	}
}
