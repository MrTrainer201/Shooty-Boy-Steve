using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeismicAI : MonoBehaviour {

	float speed = 0.5f;
	public Vector3 enemypos;

	//Health Declarations
	public int starthealth = 200;
	public int currenthealth;

	Animator anim;
	AudioSource audio;
	EnemyDeath explode;

	bool isDead;
	//bool damaged;

	bool right = false;
	bool left = true;
	public Slider healthslider;

	void Awake () 
	{
		anim = GetComponent <Animator> ();
		explode = GetComponent <EnemyDeath> ();
		audio = GetComponent <AudioSource> ();
		currenthealth = starthealth;
	}	

	void Update () 
	{
		if (healthslider == null) 
		{
			healthslider = FindObjectOfType<Slider> ();
		}

		if (!GameManager.Instance.Paused) {
			if (gameObject.transform.position.y >= 5) {
				float ypos = gameObject.transform.position.y - (Time.deltaTime * speed);
				float xpos = gameObject.transform.position.x;
				enemypos = new Vector3 (xpos, ypos, 0);
				gameObject.transform.position = enemypos;
			} else if (left) {
				float xpos = gameObject.transform.position.x - (Time.deltaTime * 0.5f);
				enemypos = new Vector3 (xpos, 5, 0);
				gameObject.transform.position = enemypos;

				if (gameObject.transform.position.x <= -1) {
					right = true;
					left = false;
				}
			} else if (right) {
				float xpos = gameObject.transform.position.x + (Time.deltaTime * 0.5f);
				enemypos = new Vector3 (xpos, 5, 0);
				gameObject.transform.position = enemypos;

				if (gameObject.transform.position.x >= 1) {
					right = false;
					left = true;
				}
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Laser_Player") 
		{
			TakeDamage (1);
		}
		else if (coll.gameObject.tag == "Nuke") 
		{
			TakeDamage (50);
		}

	}

	public void TakeDamage (int amount)
	{
		currenthealth -= amount;

		healthslider.value = currenthealth;

		if(currenthealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		anim.SetTrigger ("Bossdie");
		//audio.clip = deathClip;
		audio.Play ();

		Invoke ("Finaldie", 5);
	}

	void Finaldie ()
	{
		explode.Explode (true, true);
		Destroy (gameObject);
	}

	public bool Checkalive ()
	{
		if (isDead) 
		{
			return (false);
		} 
		else 
		{
			return (true);
		}
	}
}
