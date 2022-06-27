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
        if (validationEnabled)
            validatePosition();
    }

    bool validationEnabled = true;

    public void toggleValidation(bool validate)
    {
        validationEnabled = validate;
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

    internal void checkIfInContainer()
    {
        Vector3 deltaFromCenter = transform.position - FlaskManager.instance.containerCenter.position;
        if (deltaFromCenter.magnitude < FlaskManager.instance.maxDistanceFromCenter)
        {
            toggleValidation(true);
        }
    }
}
