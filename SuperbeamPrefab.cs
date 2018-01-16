using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperbeamPrefab : MonoBehaviour {

	public GameObject Beam;
	public GameObject Death;
	GameObject BeamClone;
	GameObject DeathClone;


	int number;
	bool alive;
	SeismicAI seismic;
	GameObject Seismicship;

	float WarningLifetime;
	float BeamLiftetime;
	GameObject Player;

	void Awake ()
	{
		Seismicship = GameObject.FindGameObjectWithTag ("Enemy");
		if (Seismicship != null) 
		{
			seismic = Seismicship.GetComponent <SeismicAI> ();
		} 
		else 
		{
			Debug.Log ("Gun reference cannot find boss");
		}

		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
	{
		if (!GameManager.Instance.Paused) 
		{
			if (BeamClone != null) 
			{
				WarningLifetime -= (Time.deltaTime);

				if (WarningLifetime <= 0) 
				{
					Destroy (BeamClone);
					Deathlaser ();
				}
			}

			if (DeathClone != null) 
			{
				BeamLiftetime -= (Time.deltaTime);

				if (BeamLiftetime <= 0) 
				{
					Destroy (DeathClone);
				}
			}
		}
	}

	void OnTriggerStay2D () 
	{
		if (!GameManager.Instance.Paused) 
		{
			number = Random.Range (1, 1000);

			if ((number == 250 || number == 500 || number == 750) && ((BeamClone == null) && (DeathClone == null)) && (Player != null)) 
			{
				alive = seismic.Checkalive ();

				if (alive) 
				{
					BeamClone = Instantiate (Beam, transform.position, Quaternion.identity) as GameObject;

					BeamClone.transform.parent = gameObject.transform;

					WarningLifetime = 2;
				}
			}
		}
	}

	void Deathlaser ()
	{
		DeathClone = Instantiate (Death, transform.position, Quaternion.identity) as GameObject;

		DeathClone.transform.parent = gameObject.transform;

		BeamLiftetime = 3;
	}

}
