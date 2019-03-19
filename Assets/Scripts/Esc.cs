using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Esc : MonoBehaviour
{

	public string levelToLoad = "";
	public GameObject menu;
	public static bool GameIsPaused = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	void Pause()
	{
		menu.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Resume()
	{
		menu.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	public void ExitGame()
	{
		SceneManager.LoadScene(levelToLoad);
		Time.timeScale = 1f;
	}

}
