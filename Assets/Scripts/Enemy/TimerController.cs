using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{

    public float startTime = 5f;
    public float currentTime;
    private bool timerRunning = false;


    private void Start()
    {
        currentTime = startTime;
        timerRunning = true;

    }

    private void Update()
    {
        if (timerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                {
                    currentTime = 0;
                    timerRunning = false;
                }
            }
        }

        if (timerRunning == false)
        {
            SceneManager.LoadScene("Level1");
        }
    }
    
    
}
