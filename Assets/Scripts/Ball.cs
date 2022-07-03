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
        if (validationEnabled) validatePosition();
        if (spoutValidationEnabled) validateSpoutPosition();
    }

    private bool collected = false;

    public void setCollected()
    {
        if (collected) return;

        collected = true;

        RemainingLabel.instance.addCollectedBall();
    }

    private bool consumed = false;

    internal void setConsumed()
    {
        if (consumed) return;

        consumed = true;
    }

    public bool isConsumed()
    {
        return consumed;
    }

    bool validationEnabled = true;

    bool spoutValidationEnabled = false;

    public void toggleValidation(bool validate)
    {
        validationEnabled = validate;
    }

    public void toggleSpoutValidation(bool validate)
    {
        spoutValidationEnabled = validate;
    }

    private void validatePosition()
    {
        Vector3 deltaFromCenter =
            transform.position - FlaskManager.instance.containerCenter.position;
        if (
            deltaFromCenter.magnitude >
            FlaskManager.instance.maxDistanceFromCenter
        )
        {
            transform.position =
                FlaskManager.instance.containerCenter.position +
                lastApprovedPosition;
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
            toggleSpoutValidation(false);
        }
    }

    internal void checkIfInFlaskSpout()
    {
        if (isInsideSpout())
        {
            toggleSpoutValidation(true);
        }
    }

    private bool insideSpout = false;

    public bool isInsideSpout()
    {
        return insideSpout;
    }

    public void setInsideSpout(bool inside)
    {
        insideSpout = inside;
    }

    public bool isInsideSphere()
    {
        Vector3 deltaFromCenter =
            transform.position - FlaskManager.instance.containerCenter.position;
        if (
            deltaFromCenter.magnitude <
            FlaskManager.instance.maxDistanceFromCenter
        )
        {
            return true;
        }
        return false;
    }

    Vector3 lastApprovedSpoutPosition;
    Vector3 rotationAtLastApprovedSpoutPosition;

    private void validateSpoutPosition()
    {
        if(isInsideSphere())
        {
            return;
        }
        if (!isInsideSpout())
        {
            // set position to last approved position, accounting for change in rotation
            Vector3 rotationDelta = FlaskManager.instance.transform.rotation.eulerAngles - rotationAtLastApprovedSpoutPosition;
            Vector3 rotationLastApprovedPosition = Quaternion.Euler(rotationDelta) * lastApprovedSpoutPosition ;
            transform.position =
                FlaskManager.instance.transform.position +
                rotationLastApprovedPosition;

        }else{
            lastApprovedPosition = transform.position - FlaskManager.instance.transform.position;
            // get euler rotation
            rotationAtLastApprovedSpoutPosition = FlaskManager.instance.transform.eulerAngles;
        }
    }
}
