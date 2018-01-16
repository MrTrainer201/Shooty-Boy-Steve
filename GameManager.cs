using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	/* Script that allows the player to pause and unpause the game */

	//A bool that determines if the game is paused
	private bool paused;
	//A GameObject reference to the player
	GameObject Player;

	//References to various UI elements
	[SerializeField]
	private GameObject PauseUI; //Pause text
	[SerializeField]
	private GameObject RetryButton; //Retry button
	[SerializeField]
	private GameObject GameoveUI; //Gameover UI which containes Gameover Text, along with Retry, Mainmenu, and Quit buttons
	[SerializeField]
	private GameObject Gameovertext; //Game over Text

	//Allows public access to paused bool
	public bool Paused
	{
		get
		{
			return paused;
		}
	}

	private static GameManager instance;

	//Allows public access to gamemanager script
	public static GameManager Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<GameManager> ();
			}

			return GameManager.instance;
		}
	}

	void Start ()
	{
		//Establish reference to the player
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		//Pause the game if the player pushed start or q and is not dead.
		if (Input.GetKeyDown ("q") || Input.GetButtonDown ("Start Button"))
		{
			if (Player != null) 
			{
				PauseGame ();
			}
		}
	}

	//Function that pauses or unpauses the game
	public void PauseGame ()
	{
		//Swaps paused bool between true and false
		paused = !paused;

		//Swap visibility of Pause and Game over UI
		PauseUI.SetActive (paused);
		GameoveUI.SetActive (paused);
		//Always hide game over text and retry button.
		Gameovertext.SetActive (false);
		RetryButton.SetActive (false);
	}
}
