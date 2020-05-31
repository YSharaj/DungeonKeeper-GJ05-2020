﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadWinScene()
    {
        SceneManager.LoadScene("Win");
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
	}

    public void QuitGame() {
        Application.Quit();
	}
}
