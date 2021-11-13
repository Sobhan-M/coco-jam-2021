using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveller : MonoBehaviour
{
    [SerializeField] Destination destination;
    [SerializeField] Sprite sprite;

    string travellerName;
    float startTime;
    float endTime;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
