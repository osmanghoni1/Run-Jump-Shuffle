using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthBar : NetworkBehaviour {

	GUIStyle healthStyle;
	GUIStyle backStyle;
	PlayerHealth playerHealth;
	
	// Update is called once per frame
	void Awake () {
		playerHealth = GetComponent<PlayerHealth> ();
	}

	void OnGUI(){
		if (!isLocalPlayer)
			return;
		
		InitStyles();

		// draw health bar background
		GUI.color = Color.black;
		GUI.backgroundColor = Color.black;
		GUI.Box(new Rect(Screen.width / 9f, Screen.height * 5f / 8f, Screen.width / 400f * PlayerHealth.maxHealth/2f, Screen.height / 30f), ".", backStyle);

		// draw health bar amount
		GUI.color = Color.green;
		GUI.backgroundColor = Color.green;
		GUI.Box(new Rect(Screen.width / 9f, (Screen.height * 5f / 8f) - 1 , Screen.width / 400f * playerHealth.health/2f, (Screen.height / 30f) - 2f), ".", healthStyle);
	}

	void InitStyles()
	{
		if( healthStyle == null )
		{
			healthStyle = new GUIStyle( GUI.skin.box );
			healthStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 1f, 0f, 1.0f ) );
		}

		if( backStyle == null )
		{
			backStyle = new GUIStyle( GUI.skin.box );
			backStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 0f, 0f, 1.0f ) );
		}
	}

	Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
}
