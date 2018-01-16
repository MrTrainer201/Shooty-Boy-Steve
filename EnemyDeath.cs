using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

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

	//Reverence to the script that updates the score display
	public Scorecontrol ScoreController;
	GameObject Scoremaster;

	void Start ()
	{
		Scoremaster = GameObject.FindGameObjectWithTag ("Scorecontrol");
		if (Scoremaster != null) 
		{
			ScoreController = Scoremaster.GetComponent <Scorecontrol> ();
		} 
		else 
		{
			Debug.Log ("Cannot find score controller");
		}
	}

	public void Explode (bool anti = false, bool score = true)
	{
		int randomnumber;
		randomnumber = Random.Range (1, 20);

		explosionclone = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;

		if (!anti) 
		{	
			if (randomnumber == 5) 
			{
				HealthC = Instantiate (Health, transform.position, Quaternion.identity) as GameObject;
			} 
			else if (randomnumber == 10) 
			{
				ShieldC = Instantiate (Shield, transform.position, Quaternion.identity) as GameObject;
			} 
			else if (randomnumber == 15) 
			{
				RocketC = Instantiate (Rocket, transform.position, Quaternion.identity) as GameObject;
			}
		}

		if (score) 
		{
			ScoreController.Addscore (scorevalue);
		}
	}
}
