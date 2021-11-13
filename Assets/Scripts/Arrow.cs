using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Intersection intersection;
    private Intersection.Directions direction;

    public Intersection.Directions GetDirection()
    {
        return direction;
    }

    public void SetDirection(Intersection.Directions newDirection)
    {
        direction = newDirection;
    }

    public Intersection GetIntersection()
    {
        return intersection;
    }

    public void SetIntersection(Intersection newIntersection)
    {
        intersection = newIntersection;
    }

    private void OnMouseDown()
    {
        intersection.Exit(direction.ToString());
    }
}
