using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	public const int maxHealth = 100;

	[SyncVar]
	public int health = maxHealth;


	public void TakeDamage(int amount){
		if (!isServer)
			return;
		if (health - amount < 0) {
			health = 0;
		}
		else
		health -= amount;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health == 0) Debug.Log("Health at 0");
	}
}
