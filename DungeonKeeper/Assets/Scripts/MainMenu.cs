using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame ()
    {    
        Debug.Log ("Quit!");
        Application.Quit();
    }

    public void PlayScrollSound() {
        FindObjectOfType<AudioManager>().Play("MenuScroll");
	}

    public void PlayButtonPushSound() {
        FindObjectOfType<AudioManager>().Play("ButtonPush");
	}
}
