using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intersection : MonoBehaviour
{
    public enum Directions {Up, Down, Left, Right}

    [SerializeField] bool hasUpButton = true;
    [SerializeField] bool hasDownButton = true;
    [SerializeField] bool hasRightButton = true;
    [SerializeField] bool hasLeftButton = true;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrowDisplacement = 32f;
    GameObject upButton = null;
    GameObject downButton = null;
    GameObject leftButton = null;
    GameObject rightButton = null;

    [SerializeField] float velocity = 10f;
    Queue<GameObject> travellers = new Queue<GameObject>();

    [SerializeField] AudioClip stoppingSoundEffect;

    private void CreateButtons()
    {
        float currentX = gameObject.transform.position.x;
        float currentY = gameObject.transform.position.y;
        float currentZ = gameObject.transform.position.z;

        Quaternion rotation = new Quaternion();


        if (hasUpButton)
        {
            upButton = Instantiate(arrow, new Vector3(currentX, currentY + arrowDisplacement, currentZ - 3), Quaternion.identity);
            upButton.GetComponent<Arrow>().SetDirection(Directions.Up);
            upButton.GetComponent<Arrow>().SetIntersection(this);
        }
        if (hasDownButton)
        {
            rotation.eulerAngles = new Vector3(0, 0, 180);
            downButton = Instantiate(arrow, new Vector3(currentX, currentY - arrowDisplacement, currentZ - 3), rotation);
            downButton.GetComponent<Arrow>().SetDirection(Directions.Down);
            downButton.GetComponent<Arrow>().SetIntersection(this);
        }
        if (hasRightButton)
        {
            rotation.eulerAngles = new Vector3(0, 0, -90);
            rightButton = Instantiate(arrow, new Vector3(currentX + arrowDisplacement, currentY, currentZ - 3), rotation);
            rightButton.GetComponent<Arrow>().SetDirection(Directions.Right);
            rightButton.GetComponent<Arrow>().SetIntersection(this);
        }
        if (hasLeftButton)
        {
            rotation.eulerAngles = new Vector3(0, 0, 90);
            leftButton = Instantiate(arrow, new Vector3(currentX - arrowDisplacement, currentY, currentZ - 3), rotation);
            leftButton.GetComponent<Arrow>().SetDirection(Directions.Left);
            leftButton.GetComponent<Arrow>().SetIntersection(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateButtons();
        DisableButtons();
    }

    private void Update()
    {
        // Dealing with the case where the traveller successfully reaches when at an intersection.
        if (travellers.Count > 0 && travellers.Peek() == null)
        {
            DisableButtons();
            travellers.Dequeue();
            if (travellers.Count > 0)
            {
                Reposition(travellers.Peek());
                EnableButtons();
            }
        }

    }

    private void Reposition(GameObject traveller)
    {
        Vector3 position = gameObject.transform.position;
        traveller.gameObject.transform.position = new Vector3(position.x, position.y, position.z - 1);
        traveller.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }    

    private void EnableButtons()
    {
        if (hasUpButton)
            upButton.gameObject.SetActive(true);
        if (hasDownButton)
            downButton.gameObject.SetActive(true);
        if (hasRightButton)
            rightButton.gameObject.SetActive(true);
        if (hasLeftButton)
            leftButton.gameObject.SetActive(true);
    }

    private void DisableButtons()
    {
        if (hasUpButton)
            upButton.gameObject.SetActive(false);
        if (hasDownButton)
            downButton.gameObject.SetActive(false);
        if (hasRightButton)
            rightButton.gameObject.SetActive(false);
        if (hasLeftButton)
            leftButton.gameObject.SetActive(false);
    }

    private void StopTraveller(GameObject traveller)
    {
        AudioSource.PlayClipAtPoint(stoppingSoundEffect, Camera.main.transform.position);
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
