using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Vector3 lastApprovedPosition;
    void Start()
    {
    }

    void Update()
    {
        validatePosition();
    }

    private void validatePosition()
    {
        Vector3 deltaFromCenter = transform.position - FlaskManager.instance.containerCenter.position;
        if (deltaFromCenter.magnitude > FlaskManager.instance.maxDistanceFromCenter)
        {
            transform.position = FlaskManager.instance.containerCenter.position + lastApprovedPosition;
        }
        else
        {
            lastApprovedPosition = deltaFromCenter;
        }
    }
}
