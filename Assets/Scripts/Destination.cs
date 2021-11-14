using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField] string destinationName;
    [SerializeField] int score;
    [SerializeField] float travellerDestroyDelay = 0f;
    [SerializeField] ScoreManager scoreManager;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Traveller traveller = collision.gameObject.GetComponent<Traveller>();
        if (traveller.GetDestination().gameObject == gameObject)
        {
            scoreManager.AddScore(score);
            traveller.ReachDestination(travellerDestroyDelay);
            StartCoroutine(CheckWinConditionAfterDelay());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Traveller traveller = collision.gameObject.GetComponent<Traveller>();
        if (traveller.GetDestination().gameObject == gameObject)
        {
            scoreManager.AddScore(score);
            traveller.ReachDestination(travellerDestroyDelay);
            StartCoroutine(CheckWinConditionAfterDelay());
        }
    }

    IEnumerator CheckWinConditionAfterDelay()
    {
        yield return new WaitForSeconds(travellerDestroyDelay + 0.5f);
        scoreManager.CheckWinCondition();
    }
}

