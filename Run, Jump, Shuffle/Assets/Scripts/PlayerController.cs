using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {
	public bool confused = false;
	public bool doubled = false;
	public bool frozen = false;
	private float dY;
	private bool jumping;
	private bool ducking;
	private float yStart;
	private SpellController sc;

	public float jumpVelocity = 0.5f;
	public float decelerationRatio = 15f;
	private int manaCount;
	public Text manaText;
	float Timer = 0.0f;

	// Use this for initialization
	void Start () {
		sc = GetComponent<SpellController> ();

		manaCount = 5;
		manaText = GetComponentInChildren<Text> ();
		manaText.text = manaCount.ToString ();
		dY = 0f;
		yStart = transform.position.y;
		jumping = false;
		ducking = false;

		Camera cam = GetComponentInChildren<Camera> ();
		if (!isLocalPlayer) {
			cam.enabled = false;
		}
		cam.gameObject.transform.parent = null;
	}

	public override void OnStartLocalPlayer(){
		GetComponent<SpriteRenderer> ().color = Color.blue;
	}

	public bool isDucking(){
		return ducking;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		if (jumpInput() && !jumping && !ducking) {
			dY = jumpVelocity;
			jumping = true;
		}
		if (duckStartInput() && !jumping && !ducking) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - .25f, transform.position.z);
			ducking = true;

		}
		if (duckStopInput() && !jumping && ducking) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + .25f, transform.position.z);
			ducking = false;
		}
		if (jumping) {
			if (transform.position.y > yStart) {
				dY -= jumpVelocity / decelerationRatio;
			} else if (transform.position.y < yStart) {
				dY = 0f;
				transform.position = new Vector3 (transform.position.x, yStart, transform.position.z);
				jumping = false;
			}
		}
		transform.Translate (new Vector3 (0f, dY));

		if (fireballInput() && manaCount >= 4){
			sc.castFireball ();
			manaCount -= 4;
		}

		//Adds Mana
		Timer += Time.deltaTime;
		if (Timer >= 3.0 && manaCount < 10) {
			manaCount++;
			Timer = 0.0f;
		}
		manaText.text = manaCount.ToString ();
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.tag == "Fireball")
			CmdTakeDamage (50);
		else
			CmdTakeDamage (10);
	}

	[Command]
	void CmdTakeDamage(int damage){
		var playerHealth = GetComponent<PlayerHealth> ();
		playerHealth.TakeDamage (damage);
	}

	bool fireballInput(){
		return Input.GetKeyDown (KeyCode.Q);
	}

	bool jumpInput(){
		if (confused)
			return Input.GetKeyDown (KeyCode.DownArrow);
		else
			return Input.GetKeyDown (KeyCode.UpArrow);
	}

	bool duckStartInput(){
		if (confused)
			return Input.GetKeyDown (KeyCode.UpArrow);
		else
			return Input.GetKeyDown (KeyCode.DownArrow);
	}

	bool duckStopInput(){
		if (confused)
			return Input.GetKeyUp (KeyCode.UpArrow);
		else
			return Input.GetKeyUp (KeyCode.DownArrow);
	}
}