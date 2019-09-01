using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRock : MonoBehaviour
{
    public float rotationX, rotationY;

    public void SetRotation(float x, float y)
    {
        rotationX = x;
        rotationY = y;
    }

    void Update()
    {
        transform.Rotate(new Vector3(rotationX, rotationY, 0) * Time.deltaTime);
    }
}
