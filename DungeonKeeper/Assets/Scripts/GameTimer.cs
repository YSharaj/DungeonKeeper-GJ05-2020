using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public TextMeshProUGUI gameTimerText;
    public float secondsRemain = 300;

    bool isCountdown = true;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTime(secondsRemain);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountdown)
        {
            secondsRemain -= Time.deltaTime;
            DisplayTime(secondsRemain);
        }
    }

    void DisplayTime(float secondsRemain)
    {
        float minutes = Mathf.FloorToInt(secondsRemain / 60);
        float seconds = Mathf.FloorToInt(secondsRemain % 60);

        gameTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (secondsRemain <= 0)
        {
            isCountdown = false;
            gameTimerText.text = string.Format("{0:00}:{1:00}", 0.0f, 0.0f);
            sceneLoader.LoadWinScene();
        }
    }
}
