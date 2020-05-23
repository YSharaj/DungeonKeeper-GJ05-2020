using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadWinScene()
    {
        SceneManager.LoadScene("Win");
    }

    public void LoadLooseScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
