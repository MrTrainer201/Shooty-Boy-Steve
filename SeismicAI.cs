using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeismicAI : MonoBehaviour {
/* Boss AI script */

	//The speed at which the ship moves
	float speed = 0.5f;
	//The position of the ship
	public Vector3 enemypos;

	//Health Start and current health variables
	public int starthealth = 200;
	public int currenthealth;

	//Reference to the boss's animator
	Animator anim;
	//Reference to boss' audio component
	AudioSource audio;
	//Reference to enemy death script
	EnemyDeath explode;

	//Bool used to determine if the boss is dead
	bool isDead;

	//Bools used to determine which direction the boss moves in
	bool right = false;
	bool left = true;
	
	//Reference to boss' health bar
	public Slider healthslider;

	//Establish all needed references
	void Awake () 
	{
		anim = GetComponent <Animator> ();
		explode = GetComponent <EnemyDeath> ();
		audio = GetComponent <AudioSource> ();
		currenthealth = starthealth;
	}	

	void Update () 
	{
		//If health bar is not already referenced, assign reference to health bar
		if (healthslider == null) 
		{
			healthslider = FindObjectOfType<Slider> ();
		}
		
		//Only executed if the game is not paused
		if (!GameManager.Instance.Paused) {
			//If the ship is too high above the screen, move down.
			if (gameObject.transform.position.y >= 5) {
				float ypos = gameObject.transform.position.y - (Time.deltaTime * speed);
				float xpos = gameObject.transform.position.x;
				enemypos = new Vector3 (xpos, ypos, 0);
				gameObject.transform.position = enemypos;
			//If the ship should be moving left, move left
			} else if (left) {
				float xpos = gameObject.transform.position.x - (Time.deltaTime * 0.5f);
				enemypos = new Vector3 (xpos, 5, 0);
				gameObject.transform.position = enemypos;

				//If the ship goes past x = -1, begin moving right
				if (gameObject.transform.position.x <= -1) {
					right = true;
					left = false;
				}
			//If the ship should be moving right, move right
			} else if (right) {
				float xpos = gameObject.transform.position.x + (Time.deltaTime * 0.5f);
				enemypos = new Vector3 (xpos, 5, 0);
				gameObject.transform.position = enemypos;

				//If the ship goes past x = 1, begin moving left
				if (gameObject.transform.position.x >= 1) {
					right = false;
					left = true;
				}
			}
		}
	}

	//Deal with collisions
	void OnCollisionEnter2D (Collision2D coll)
	{
		//If the ship is hit by a player's laser, take 1 damage
		if (coll.gameObject.tag == "Laser_Player") 
		{
			TakeDamage (1);
		}
		//If the ship is hit by a missile, take 50 damage
		else if (coll.gameObject.tag == "Nuke") 
		{
			TakeDamage (50);
		}

	}

	//Function used to register damage
	public void TakeDamage (int amount)
	{
		//Reduce current health by damage amount
		currenthealth -= amount;

		//Update health bar
		healthslider.value = currenthealth;

		//If the boss has 0 health, kill it.
		if(currenthealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	//Function that activates the boss's death animation
	void Death ()
	{
		isDead = true;

		//Trigger boss death animation
		anim.SetTrigger ("Bossdie");
		//Activate explosion noise
		audio.Play ();

		//Call the final death function
		Invoke ("Finaldie", 5);
	}

	//Function that kills the boss
	void Finaldie ()
	{
		//Create explosion and destroy the boss
		explode.Explode (true, true);
		Destroy (gameObject);
	}

	//Function used by weapon instantiators to determine if the boss is still alive
	public bool Checkalive ()
	{
		//If boss has no health left, return false.
		if (isDead) 
		{
			return (false);
		} 
		//Otherwise, return true
		else 
		{
			return (true);
		}
	}
}
