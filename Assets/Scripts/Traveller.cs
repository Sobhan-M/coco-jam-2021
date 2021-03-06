using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traveller : MonoBehaviour
{
    [SerializeField] Destination destination;
    [SerializeField] Image destinationImage;
    [SerializeField] float yVelocity = 0f;
    [SerializeField] float xVelocity = 2f;
    [SerializeField] Sprite check;
    [SerializeField] GameObject confetti;
    [SerializeField] bool isEndless = false;
    [SerializeField] int numOfStops = 1;
    [SerializeField] AudioClip soundEffect;


    string travellerName;
    float startTime;
    float endTime;

    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);
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
        destinationImage.sprite = destination.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void ReachDestination(float delay)
    {
        AudioSource.PlayClipAtPoint(soundEffect, Camera.main.transform.position);
        --numOfStops;
        RandomNewDestination();
        UpdateDestinationImage();
        if (!isEndless && numOfStops <= 0)
        {
            DestroyTraveller(delay);
        }
    }

    private void DestroyTraveller(float delay)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        destinationImage.sprite = check;
        var effects = Instantiate(confetti, transform.position, Quaternion.identity);
        Destroy(effects, delay + 2f);
        Destroy(gameObject, delay);
    }




}
