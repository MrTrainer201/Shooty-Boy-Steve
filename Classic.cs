using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classic : MonoBehaviour {
/* This script handles player movement, collisions, and invincibility */
	
	//Variable for movement speed
	float shipspeed = 10;
	//Variable to hold player's position.
	public Vector3 PlayerP;
	//Gameobject reference to the player
	GameObject player;
	//Reference to player health script.
	Playerhealth PlayH;
	//Gameobject reference to the player's gun
	GameObject Gun;
	//Reference to shooting script
	LaserPrefab Ammo;

	//Bool that determines damage immunity.
	bool immune = false;
	//Bool that deals with immunity overlap (In the case of multiple immunity power-up)
	bool Pureimmune = false;

	//Reference to the ships's animator
	Animator anim;

	//Reference to shield sound effect
	public GameObject Shieldsound;
	GameObject Soundclone;

	//Variables that help control shield timer and countdown UI
	float Shieldtimer;
	int Shieldtimerint;
	Text Countdowntext;
	public GameObject Countdowntimer; 

	void Awake()
	{
		//Establish reference to "Player" gameobject.
		player = GameObject.FindGameObjectWithTag ("Player");
		//Establish reference to "Playerhealth" script.
		PlayH = player.GetComponent <Playerhealth> ();
		//Establish reference to "Gun" gameobject.
		Gun = GameObject.FindGameObjectWithTag ("Playergun");
		//Establish reference to the "Laserprefab" script.
		Ammo = Gun.GetComponent <LaserPrefab> ();
		//Establish reference to the Ship's animator
		anim = GetComponent <Animator> ();
	}

	void Update () 
	{
		//Only executes if the game is not paused
		if (!GameManager.Instance.Paused) 
		{
			/* Movement */ 
			//Update x and y positions
			float xpos = gameObject.transform.position.x + (Input.GetAxis ("Horizontal") * shipspeed * Time.deltaTime);
			float ypos = gameObject.transform.position.y + (Input.GetAxis ("Vertical") * shipspeed * Time.deltaTime);

			//Update player position based on input (X-axis restriction: -5,5 Y-asix restions: -4.5,4.5)
			PlayerP = new Vector3 (Mathf.Clamp (xpos, -5, 5), Mathf.Clamp (ypos, -4.5f, 4.5f), 0);
			gameObject.transform.position = PlayerP;


			/* Invincibility */
			//If the player has an active shield, execute this code
			if (Pureimmune) 
			{
				//Display the shield timer UI
				Countdowntimer.SetActive (true);
			
				//Establish connection to countdown UI if reference is not active
				if (Countdowntext == null) 
				{
					Countdowntext = Countdowntimer.GetComponent <Text> ();
				}

				//Decrement the shield timer
				Shieldtimer -= (Time.deltaTime);

				//Round the shield timer number to an integer
				Shieldtimerint = Mathf.CeilToInt (Shieldtimer);

				//Update the countdown UI to match the current int value for the shield timer
				Countdowntext.text = Shieldtimerint + " ";

				//If the timer is at 0, remove player's immunity
				if (Shieldtimer <= 0) 
				{
					Vincible ();
				}
			}
		}
	}

	//Handles object collision with the player
	void OnCollisionEnter2D(Collision2D coll)
	{ 
		if ((coll.gameObject.tag == "Power_Enemy") && (!immune)) 
		{
			PlayH.TakeDamage (5);
			immune = true;
			Invoke ("IV_Frames", 1.5f);
		} 
		else if ((coll.gameObject.tag == "Laser_Enemy") && (!immune)) 
		{
			PlayH.TakeDamage (1);
			immune = true;
			Invoke ("IV_Frames", 1.5f);
		} 
		else if ((coll.gameObject.tag == "Enemy") && (!immune)) 
		{
			PlayH.TakeDamage (10);
		} 
		else if (coll.gameObject.tag == "Health") 
		{
			PlayH.TakeDamage (-5, true);
		} 
		else if (coll.gameObject.tag == "Shield") 
		{
			Invincibility ();
		} 
		else if (coll.gameObject.tag == "Rocket") 
		{
			Ammo.Getammo ();
		}
	

	}

	//Function that makes the player able to be hit after IV frames run out
	void IV_Frames ()
	{
		//If the player is not pure immune (I.E. has an invincibilty power active) remove immunity 
		if (!Pureimmune) 
		{
			immune = false;
		}
	}

	//Function that makes the player invincible after picking up an invincibility power-up
	void Invincibility ()
	{
		//Player shield animation
		anim.SetTrigger ("Shield");

		//Play shield sound effect
		Soundclone = Instantiate (Shieldsound, transform.position, Quaternion.identity) as GameObject;

		//Make the player immune
		Pureimmune = true;
		immune = true;

		//Max out shield timer
		Shieldtimer = 10;


	}

	//Function that removes a player's invinciblity shield
	void Vincible ()
	{
		//Switch to standard animation
		anim.SetTrigger ("NoShield");

		//Remove any player immunity
		Pureimmune = false;
		immune = false;

		//Remove timer UI
		Countdowntimer.SetActive (false);
	}
}
