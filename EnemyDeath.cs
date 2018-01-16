using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
/* Script that handles enemy death */
	
	//Object used for explosion animation
	public GameObject explosion;
	GameObject explosionclone;

	//Item pickup objects
	public GameObject Health;
	GameObject HealthC;
	public GameObject Shield;
	GameObject ShieldC;
	public GameObject Rocket;
	GameObject RocketC;

	//Score value that is added when an enemy dies.
	public int scorevalue;

	//Reference to the script that updates the score display
	public Scorecontrol ScoreController;
	GameObject Scoremaster;

	void Start ()
	{
		//Establish connection to the object that contains the score control script
		Scoremaster = GameObject.FindGameObjectWithTag ("Scorecontrol");
		
		//Once the object is found, establish reference to the score contrl script
		if (Scoremaster != null) 
		{
			ScoreController = Scoremaster.GetComponent <Scorecontrol> ();
		} 
		//Otherwise print an error message
		else 
		{
			Debug.Log ("Cannot find score controller");
		}
	}

	//Function that destroys an enemy
	/*
	Parameters:
	A bool "anti" that determines if the enemy is an Anti and a special ship
	A bool "score" that determins if a ship was destroyed by the player or simply despawned
	*/
	public void Explode (bool anti = false, bool score = true)
	{
		//Select a random number
		int randomnumber;
		randomnumber = Random.Range (1, 20);

		explosionclone = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;

		//If the ship is an Anti, don't drop any items, otherwise determine if an item should be dropped
		if (!anti) 
		{	
			//Drops a health pickup
			if (randomnumber == 5) 
			{
				HealthC = Instantiate (Health, transform.position, Quaternion.identity) as GameObject;
			} 
			//Drops a shield pickup
			else if (randomnumber == 10) 
			{
				ShieldC = Instantiate (Shield, transform.position, Quaternion.identity) as GameObject;
			} 
			//Drops a rocket pickup
			else if (randomnumber == 15) 
			{
				RocketC = Instantiate (Rocket, transform.position, Quaternion.identity) as GameObject;
			}
		}

		//If the enemy did not self destruct, add this enemy's score value to total score.
		if (score) 
		{
			ScoreController.Addscore (scorevalue);
		}
	}
}
