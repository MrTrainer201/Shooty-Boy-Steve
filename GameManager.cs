using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private bool paused;
	GameObject Player;

	[SerializeField]
	private GameObject PauseUI;
	[SerializeField]
	private GameObject RetryButton;
	[SerializeField]
	private GameObject GameoveUI;
	[SerializeField]
	private GameObject Gameovertext;


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
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("q") || Input.GetButtonDown ("Start Button"))
		{
			if (Player != null) 
			{
				PauseGame ();
			}
		}
	}

	public void PauseGame ()
	{
		paused = !paused;

		PauseUI.SetActive (paused);
		GameoveUI.SetActive (paused);
		Gameovertext.SetActive (false);
		RetryButton.SetActive (false);
	}
}
