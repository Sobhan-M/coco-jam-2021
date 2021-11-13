using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField] string destinationName;
    [SerializeField] int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Traveller traveller = collision.gameObject.GetComponent<Traveller>();
        Debug.Log("The traveller is here!");
        if (traveller.GetDestination().gameObject == gameObject)
        {
            Debug.Log("The traveller is here!");
            // TODO: Score point.
            traveller.ReachDestination();
        }
    }
}
