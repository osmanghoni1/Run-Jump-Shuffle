using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellController : NetworkBehaviour {
	public int confusedMaxDuration = 1000;
	public int doubledMaxDuration = 300;
	public int frozenMaxDuration = 300;

	private int confusedDurationRemaining = 0;
	private int doubledDurationRemaining = 0;
	private int frozenDurationRemaining = 0;

	private PlayerController pc;

	void Start(){
		pc = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (pc.confused) {
			confusedDurationRemaining--;
			if (confusedDurationRemaining <= 0 && !pc.isDucking())
				pc.confused = false;
		}
		if (pc.doubled) {
			doubledDurationRemaining--;
			if (doubledDurationRemaining <= 0)
				pc.doubled = false;
		}
		if (pc.frozen) {
			frozenDurationRemaining--;
			if (frozenDurationRemaining <= 0)
				pc.frozen = false;
		}
	}

	//cast functions called when key is pressed and mana is spent

	//frozen is client side only
	public void castFrozen(){
		pc.frozen = true;
		frozenDurationRemaining = frozenMaxDuration;
	}

	public void castDoubled(){
		CmdDouble (pc.netId);
	}

	public void castConfuse(){
		CmdConfuse (pc.netId);
	}
	public void castFireball(){
		CmdFireball (pc.netId);
	}

	//send command to server
	[Command]
	void CmdDouble(NetworkInstanceId id){
		RpcHitByDoubled (id);
	}

	[Command]
	void CmdConfuse(NetworkInstanceId id){
		RpcHitByConfused (id);
	}

	[Command]
	void CmdFireball(NetworkInstanceId id){
		RpcHitByFireball (id);
	}

	//recieve command to serveer, activate if this wasnt the instance that sent the command
	[ClientRpc]
	public void RpcHitByDoubled(NetworkInstanceId id){
		if (pc.netId != id) {
			pc.doubled = true;
			doubledDurationRemaining = doubledMaxDuration;
		}
	}

	[ClientRpc]
	public void RpcHitByConfused(NetworkInstanceId id){
		if (pc.netId != id) {
			pc.confused = true;
			confusedDurationRemaining = confusedMaxDuration;
		}
	}

	[ClientRpc]
	public void RpcHitByFireball(NetworkInstanceId id){
		if (pc.netId != id) {
			GetComponent<FireballSpawner> ().spawn ();
		}
	}
}
