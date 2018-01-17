using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonAI : MonoBehaviour {
	/* AI for Lock-On (Lime Green) Ships*/

	//A variable that holds the speed at which the ship turns
	public float Turnspeed = 10;
	//The position of the ship
	public Vector3 Shippos;
	//The position of the player
	public Vector3 Targetpos;
	//Bool that determins if the ship has 0 health or not
	bool isDead;
	//An int that represents current health
	public int currenthealth;
	//An int that represents max health
	public int starthealth = 5;
	//Reference the the enemy death script
	EnemyDeath explode;
	//Reference to the player
	GameObject Player;
	//Reference to targer to aim at.
	public Transform Target;

	//Instantiate all references
	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		explode = GetComponent <EnemyDeath> ();
		currenthealth = starthealth;
		Target = Player.transform;
	}

	void Update ()
	{
		//Only executed if the game is not paused
		if (!GameManager.Instance.Paused) 
		{
			//If the player is still active, lock on to them
			if (Player != null)
			{
				Vector3 vectorToTarget = Target.position - transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * Turnspeed);
			}
		}
	}

	//Collisions
	void OnCollisionEnter2D(Collision2D coll)
	{
		//If the ship is hit by a laser, take one damage.
		if (coll.gameObject.tag == "Laser_Player") 
		{
			TakeDamage (1);
		} 
		//If the ship is damaged by any other means, die instantly.
		else if ((coll.gameObject.tag == "Player") || (coll.gameObject.tag == "Despawn") || (coll.gameObject.tag == "Nuke")) 
		{
			explode.Explode ();
			Destroy (gameObject);
		}
	}

	//Function that adds damage
	void TakeDamage (int amount)
	{
		//Decrement current health by damage amount
		currenthealth -= amount;

		//If current health is depleted to 0, call death function
		if(currenthealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	//Function that kills a ship
	void Death ()
	{
		isDead = true;

		//Call the explosion function in the Enemy Death script
		explode.Explode ();
		//Destroy the game object
		Destroy (gameObject);

	}
}
