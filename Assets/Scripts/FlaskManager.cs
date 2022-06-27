using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : GenericSingleton<FlaskManager>
{

    float requestedRotation = 0;
    void Start()
    {
    }

    public void addRotation(float rotation)
    {
        requestedRotation += rotation;
    }

    public float useRotation()
    {
        float rotation = requestedRotation;
        requestedRotation = 0;
        return rotation;
    }

    public void FixedUpdate()
    {
        transform.Rotate(0, 0, useRotation());
    }
}
