using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float elapsedTime = 0;
    private bool timerIsRunning = false;
    private UIManager uIManager;
    private GameManager gameManager;

    public void Init()
    {
        timerIsRunning = true;
        uIManager = UIManager.Instance;
        gameManager = GameManager.Instance;
    }

    public void DeInit()
    {
        timerIsRunning = false;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            elapsedTime += Time.deltaTime;
            DisplayTime(elapsedTime);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        uIManager.TimerText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
