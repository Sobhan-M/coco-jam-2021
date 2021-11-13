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
    GameObject traveller = null;

    // Start is called before the first frame update
    void Start()
    {
        DisableButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reposition()
    {
        traveller.gameObject.transform.position = gameObject.transform.position;
    }    

    public void EnableButtons()
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

    public void DisableButtons()
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

    public void StopTraveller()
    {
        traveller.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void RestartTraveller(Directions direction)
    {
        Debug.Log(direction);
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
        traveller = collision.gameObject;
        StopTraveller();
        EnableButtons();
    }

    public void Exit(string directionString)
    {
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
        Reposition();
        RestartTraveller(direction);
        DisableButtons();
    }
}
