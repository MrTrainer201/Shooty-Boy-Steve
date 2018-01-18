using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperbeamPrefab : MonoBehaviour {
/* Script that controls the boss' giant death beam */
	
	//Game objects for the warning laser and damage laser.
	public GameObject Beam;
	public GameObject Death;
	GameObject BeamClone;
	GameObject DeathClone;

	//An int used to hold a random number
	int number;
	//A bool used to determine if the boss is alive
	bool alive;
	//Reference to Boss' AI script
	SeismicAI seismic;
	//Reference to the boss itself.
	GameObject Seismicship;

	//The lifespan of the warning laser
	float WarningLifetime;
	//The lifespan of the death beam
	float BeamLiftetime;
	//Reference to the player
	GameObject Player;

	void Awake ()
	{
		//Establish reference to the boss
		Seismicship = GameObject.FindGameObjectWithTag ("Enemy");
		if (Seismicship != null) 
		{
			seismic = Seismicship.GetComponent <SeismicAI> ();
		} 
		else 
		{
			Debug.Log ("Gun reference cannot find boss");
		}

		//Establish reference to the player
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
	{
		//Only executed if the game is not paused.
		if (!GameManager.Instance.Paused) 
		{
			//Only executed if there is an active warning laser
			if (BeamClone != null) 
			{
				//Decrement lifetime of warning laser
				WarningLifetime -= (Time.deltaTime);

				//Once lifetime is depleted created death beam
				if (WarningLifetime <= 0) 
				{
					Destroy (BeamClone);
					Deathlaser ();
				}
			}

			//Only executed if there is an active death beam
			if (DeathClone != null) 
			{
				//Decrement lifetime of death beam
				BeamLiftetime -= (Time.deltaTime);

				//Once lifetime is depleted, remove the death beam
				if (BeamLiftetime <= 0) 
				{
					Destroy (DeathClone);
				}
			}
		}
	}

	//Whice the enemy is within the valid attack range zone, try to spawn lasers
	void OnTriggerStay2D () 
	{
		//Only executed if the game is not paused
		if (!GameManager.Instance.Paused) 
		{
			//Select a random number from 1 - 1000
			number = Random.Range (1, 1000);

			//If random number is 250, 500, or 750 AND there is not already an active laser AND the player is alive, spawn a laser
			if ((number == 250 || number == 500 || number == 750) && ((BeamClone == null) && (DeathClone == null)) && (Player != null)) 
			{
				//Check if the boss is alive
				alive = seismic.Checkalive ();

				//If the boss is still alive, spawn a laser
				if (alive) 
				{
					//Spawn new warning beam
					BeamClone = Instantiate (Beam, transform.position, Quaternion.identity) as GameObject;

					//Have beam move with boss ship
					BeamClone.transform.parent = gameObject.transform;

					//Set lifetime of warning laser
					WarningLifetime = 2;
				}
			}
		}
	}

	//Function that allows warning laser to spawn death beams
	void Deathlaser ()
	{
		//Create death beam
		DeathClone = Instantiate (Death, transform.position, Quaternion.identity) as GameObject;

		//Allow death beam to move with boss ship
		DeathClone.transform.parent = gameObject.transform;

		//Set lifetime for death beam
		BeamLiftetime = 3;
	}

}
