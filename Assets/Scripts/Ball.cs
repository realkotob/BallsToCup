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

    private bool collected = false;
    public void setCollected()
    {
        if (collected)
            return;

        collected = true;

        RemainingLabel.instance.addCollectedBall();
    }

    private bool consumed = false;
    internal void setConsumed()
    {
        if (consumed)
            return;

        consumed = true;
    }

    public bool isConsumed(){
        return consumed;
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

    internal void checkIfInFlaskSphere()
    {
        if (isInsideSphere())
        {
            toggleValidation(true);
        }
    }

    public bool isInsideSphere()
    {
        Vector3 deltaFromCenter = transform.position - FlaskManager.instance.containerCenter.position;
        if (deltaFromCenter.magnitude < FlaskManager.instance.maxDistanceFromCenter)
        {
            return true;
        }
        return false;
    }
}
