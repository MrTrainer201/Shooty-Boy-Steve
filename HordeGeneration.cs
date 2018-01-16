using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HordeGeneration : MonoBehaviour {
/* Script that handles the generation of hordes of enemies */
	
	//Game object reference to the current spawned horde
	GameObject horde; 
	//An int that represents the current wave
	public int hordenumber = 0;
	//An int that represents the number of bosses spawned
	int bossnumber = 0;
	//An int that represents the number used to determine what wave to spawn
	int randomnumber;
	//A bool that determines if there is currently a wave spawned in.
	bool spawned;

	//Game object reference to standard level music
	public GameObject Levelmusic;
	GameObject MusicClone;

	//Game object reference to wave text UI and wave text animator
	public GameObject Wavecounttext;
	Text Wavetext;
	Animator Waveanim;

	//Variables for potential combinations that can be instantiated
	public GameObject combo_1;
	GameObject Ccombo_1;
	public GameObject combo_2;
	GameObject Ccombo_2;
	public GameObject combo_3;
	GameObject Ccombo_3;
	public GameObject combo_4;
	GameObject Ccombo_4;
	public GameObject combo_5;
	GameObject Ccombo_5;
	public GameObject combo_6;
	GameObject Ccombo_6;
	public GameObject combo_7;
	GameObject Ccombo_7;
	public GameObject combo_8;
	GameObject Ccombo_8;
	public GameObject combo_9;
	GameObject Ccombo_9;
	public GameObject combo_10;
	GameObject Ccombo_10;
	public GameObject combo_11;
	GameObject Ccombo_11;
	public GameObject combo_12;
	GameObject Ccombo_12;
	public GameObject combo_13;
	GameObject Ccombo_13;
	public GameObject combo_14;
	GameObject Ccombo_14;
	public GameObject combo_15;
	GameObject Ccombo_15;
	public GameObject combo_16;
	GameObject Ccombo_16;
	public GameObject Boss;
	GameObject BossClone;
	public GameObject Boss2;
	GameObject BossClone2;
	public GameObject Boss3;
	GameObject BossClone3;

	//References to various boss health meters.
	[SerializeField]
	private GameObject Boss1health;
	[SerializeField]
	private GameObject Boss2health;
	[SerializeField]
	private GameObject Boss3health;

	//Establish connection to the wave text
	void Awake ()
	{
		Wavetext = Wavecounttext.GetComponent <Text> ();
		Waveanim = Wavecounttext.GetComponent <Animator> ();
	}

	void Update () 
	{
		//Establish reference to the current wave of enemies, if any are active
		horde = GameObject.FindGameObjectWithTag ("Combo");

		//Set the bool for a current wave being spawned to false.
		spawned = false;

		//Update the current wave
		Wavetext.text = "Wave " + hordenumber;

		//If the current wave is a boss wave, change text to Boss 
		if (hordenumber % 10 == 0) 
		{
			Wavetext.text = "Boss " + bossnumber;
		}

		//If there is no boss or standard level music currently active, spawn new instance of level music.
		if ((BossClone == null) && (BossClone2 == null) && (BossClone3 == null) && (MusicClone == null)) 
		{
			MusicClone = Instantiate (Levelmusic, transform.position, Quaternion.identity) as GameObject;
		}

		//If a boss is not active, remove the boss healthbar UI
		if ((BossClone == null) && (BossClone2 == null) && (BossClone3 == null)) 
		{
			Boss1health.SetActive (false);
			Boss2health.SetActive (false);
			Boss3health.SetActive (false);
		}

		//If there are currently no enemies on screen, spawn new enemies
		if ((horde == null) && (BossClone == null) && (BossClone2 == null) && (BossClone3 == null)) 
		{
			//After boss 4, limit possible combinations to 12-16
			if(hordenumber > 39)
			{
				randomnumber = Random.Range (20, 27);
			}
			//After boss 3, limit possible combinations to 8-16
			else if (hordenumber > 29)
			{
				randomnumber = Random.Range (12,27);
			}
			//After boss 2, limit possible combinations to 5-16
			else if (hordenumber > 19)
			{
				randomnumber = Random.Range (8, 27);
			}
			//Otherwise, allow potential for all combinations, except for minimum wave requirement.
			else
			{
				randomnumber = Random.Range (1, 27);
			}

			//Increase wave number
			hordenumber++;

			//If it is wave 1, always spawn combination 1.
			if (hordenumber == 1) 
			{
				Ccombo_1 = Instantiate (combo_1, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
			} 
			//If it's wave 10, spawn the Level 1 Boss
			else if(hordenumber == 10)
			{
				BossClone = Instantiate (Boss, transform.position, Quaternion.identity) as GameObject;
				Boss1health.SetActive (true);
				spawned = true;
				bossnumber++;
				Waveanim.SetTrigger ("Nextwave");
			}
			//If it's wave 20, spawn the Level 2 Boss
			else if (hordenumber == 20)
			{
				BossClone2 = Instantiate (Boss2, transform.position, Quaternion.identity) as GameObject;
				Boss2health.SetActive (true);
				spawned = true;
				bossnumber++;
				Waveanim.SetTrigger ("Nextwave");
			}
			//If wave number is any other multiple of 10, spawn a Level 3 boss
			else if (hordenumber % 10 == 0) 
			{
				BossClone3 = Instantiate (Boss3, transform.position, Quaternion.identity) as GameObject;
				Boss3health.SetActive (true);
				spawned = true;
				bossnumber++;
				Waveanim.SetTrigger ("Nextwave");
			} 
			/* If it is not wave 1 or a boss wave, spawn enemy combinations based on random number */
			else if (randomnumber == 1) 
			{
				Ccombo_1 = Instantiate (combo_1, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if (randomnumber == 2 || randomnumber == 3) 
			{
				if (hordenumber <= 10) //Only spawns within the first 10 waves
				{
					Ccombo_2 = Instantiate (combo_2, transform.position, Quaternion.identity) as GameObject;
					spawned = true;
					Waveanim.SetTrigger ("Nextwave");
				} 
				else if (hordenumber >= 11) //Only spawns after 1st boss
				{
					Ccombo_7 = Instantiate (combo_7, transform.position, Quaternion.identity) as GameObject;
					spawned = true;
					Waveanim.SetTrigger ("Nextwave");
				}
			} 
			else if (randomnumber == 4 || randomnumber == 5) 
			{
				Ccombo_3 = Instantiate (combo_3, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if (randomnumber == 6 || randomnumber == 7) 
			{
				Ccombo_4 = Instantiate (combo_4, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 8 || randomnumber == 9) && (hordenumber >= 10)) //Only spawns after 1st boss
			{
				Ccombo_5 = Instantiate (combo_5, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 10 || randomnumber == 11) && (hordenumber >= 5)) //Only spawns after wave 5
			{
				Ccombo_6 = Instantiate (combo_6, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 12 || randomnumber == 13) && (hordenumber >= 11)) //Only spawns after 1st boss
			{
				Ccombo_8 = Instantiate (combo_8, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 14 || randomnumber == 15) && (hordenumber >= 11)) //Only spawns after 1st boss
			{
				Ccombo_9 = Instantiate (combo_9, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 16 || randomnumber == 17) && (hordenumber >= 11)) //Only spawns after 1st boss
			{
				Ccombo_10 = Instantiate (combo_10, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 18 || randomnumber == 19) && (hordenumber >= 11)) //Only spawns after 1st boss
			{
				Ccombo_11 = Instantiate (combo_11, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 20 || randomnumber == 21) && (hordenumber >= 15)) //Only spawns after wave 15
			{
				Ccombo_12 = Instantiate (combo_12, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			} 
			else if ((randomnumber == 22) && (hordenumber >= 20)) //Only spawns after 2nd boss
			{
				Ccombo_13 = Instantiate (combo_13, transform.position, Quaternion.identity) as GameObject; 
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			}
			else if ((randomnumber == 23 || randomnumber == 24) && (hordenumber >= 20)) //Only spawns after 2nd boss
			{
				Ccombo_14 = Instantiate (combo_14, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			}
			else if ((randomnumber == 25 || randomnumber == 26) && (hordenumber >= 25)) //Only spawns after wave 25
			{
				Ccombo_15 = Instantiate (combo_15, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			}
			else if ((randomnumber == 27) && (hordenumber >= 25)) //Only spawns after wave 25
			{
				Ccombo_16 = Instantiate (combo_16, transform.position, Quaternion.identity) as GameObject;
				spawned = true;
				Waveanim.SetTrigger ("Nextwave");
			}
	
			//If an invalid number is used and a wave fails to spawn, decrement wave number
			if (!spawned) 
			{
				hordenumber--;
			}
		}
	}
}
