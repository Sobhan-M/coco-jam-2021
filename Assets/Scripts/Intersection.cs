using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intersection : MonoBehaviour
{
    public enum Directions {Up, Down, Left, Right}

    [SerializeField] Button upButton = null;
    [SerializeField] Button downButton = null;
    [SerializeField] Button rightButton = null;
    [SerializeField] Button leftButton = null;

    [SerializeField] float velocity = 10f;
    Queue<GameObject> travellers = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DisableButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reposition(GameObject traveller)
    {
        traveller.gameObject.transform.position = gameObject.transform.position;
    }    

    private void EnableButtons()
    {
        if (upButton != null)
            upButton.gameObject.SetActive(true);
        if (downButton != null)
            downButton.gameObject.SetActive(true);
        if (rightButton != null)
            rightButton.gameObject.SetActive(true);
        if (leftButton != null)
            leftButton.gameObject.SetActive(true);
    }

    private void DisableButtons()
    {
        if (upButton != null)
            upButton.gameObject.SetActive(false);
        if (downButton != null)
            downButton.gameObject.SetActive(false);
        if (rightButton != null)
            rightButton.gameObject.SetActive(false);
        if (leftButton != null)
            leftButton.gameObject.SetActive(false);
    }

    private void StopTraveller(GameObject traveller)
    {
        traveller.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private void RestartTraveller(GameObject traveller, Directions direction)
    {
        if (direction == Directions.Up)
            traveller.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity * Time.deltaTime);
        if (direction == Directions.Down)
            traveller.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity * Time.deltaTime);
        if (direction == Directions.Right)
            traveller.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * Time.deltaTime, 0);
        if (direction == Directions.Left)
            traveller.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        travellers.Enqueue(collision.gameObject);
        StopTraveller(collision.gameObject);
        Reposition(travellers.Peek());
        EnableButtons();
    }

    public void Exit(string directionString)
    {
        GameObject traveller = travellers.Dequeue();
        Directions direction;
        switch (directionString)
        {
            case "Up":
                direction = Directions.Up;
                break;
            case "Down":
                direction = Directions.Down;
                break;
            case "Right":
                direction = Directions.Right;
                break;
            case "Left":
                direction = Directions.Left;
                break;
            default:
                Debug.Log("Intersection.Exit(): Hit Default Case");
                direction = Directions.Up;
                break;
        }
        RestartTraveller(traveller, direction);
        if (travellers.Count == 0)
            DisableButtons();
        else
            Reposition(travellers.Peek());
    }
}
