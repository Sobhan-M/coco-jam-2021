using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traveller : MonoBehaviour
{
    [SerializeField] Destination destination;
    [SerializeField] Image destinationImage;
    [SerializeField] float speed = 20f;
    

    string travellerName;
    float startTime;
    float endTime;

    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * Time.deltaTime);
        RandomNewDestination();
        UpdateDestinationImage();
    }

    public Destination GetDestination()
    {
        return destination;
    }

    private void RandomNewDestination()
    {
        Destination[] possibleDestinations = FindObjectsOfType<Destination>();
        Destination oldDestination = destination;
        int randomOption;
        while (destination == oldDestination) // Not the most efficient, but who cares.
        {
            randomOption = (int)Random.Range(0, possibleDestinations.Length);
            destination = possibleDestinations[randomOption];
        }
    }

    private void UpdateDestinationImage()
    {
        Debug.Log(destinationImage.sprite);
        Debug.Log(destination.gameObject.GetComponent<SpriteRenderer>().sprite);
        destinationImage.sprite = destination.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void ReachDestination()
    {
        RandomNewDestination();
        UpdateDestinationImage();
    }




}
