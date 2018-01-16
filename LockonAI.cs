using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonAI : MonoBehaviour {

	public float speed = .015f;
	public float Turnspeed = 10;
	public Vector3 Shippos;
	public Vector3 Targetpos;
	bool damaged;
	bool isDead;
	public int currenthealth;
	public int starthealth = 5;
	EnemyDeath explode;
	GameObject Player;

	public Transform Target;

	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		explode = GetComponent <EnemyDeath> ();
		currenthealth = starthealth;
		Target = Player.transform;
	}

	void Update ()
	{
		if (!GameManager.Instance.Paused) 
		{
			if (Player != null)
			{
				Vector3 vectorToTarget = Target.position - transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * Turnspeed);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Laser_Player") 
		{
			TakeDamage (1);
		} 
		else if ((coll.gameObject.tag == "Player") || (coll.gameObject.tag == "Despawn") || (coll.gameObject.tag == "Nuke")) 
		{
			explode.Explode ();
			Destroy (gameObject);
		}
	}

	void TakeDamage (int amount)
	{
		damaged = true;

		currenthealth -= amount;

		//playerAudio.Play ();

		if(currenthealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		//anim.SetTrigger ("Die");

		//playeraudio.clip = deathClip;
		//playerAudio.Play ();

		explode.Explode ();
		Destroy (gameObject);

	}
}
