using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleController : NetworkBehaviour {
	public float speed = -2.25f;
	float newSpeed;
	public PlayerController playerController;

	// Use this for initialization
	void Start(){
		playerController = GetComponent<PlayerController> ();
		GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, 0f);
		newSpeed = speed;
	}
	void Update(){
		if (playerController.frozen) {
			newSpeed = 0f;
		}
		else {
			newSpeed = speed;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2 (newSpeed, 0f);
	}
	void OnTriggerEnter2D(Collider2D collision){
		if (collision.tag == "Player")
			Destroy (this.gameObject);
	}
}
