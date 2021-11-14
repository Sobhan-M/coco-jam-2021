using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int timeInSeconds = 60;
    [SerializeField] LevelSystem levels;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] int losingDelay = 2;
    private int score = 0;
    private float startTime;
    private float currentTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (timeText != null)
        {
            currentTime = Time.time;
            timeText.text = (timeInSeconds - currentTime + startTime).ToString("0.00") + " s";
            if (currentTime - startTime > timeInSeconds + losingDelay)
            {
                levels.GameOver();
            }
        }
    }

    public void CheckWinCondition()
    {
        int remainingTravellers = FindObjectsOfType<Traveller>().Length;
        Debug.Log("Remaining Travellers = " + remainingTravellers);
        if (remainingTravellers <= 0)
        {
            Win();
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    private void Win()
    {
        levels.LoadNextLevel();
    }
}
