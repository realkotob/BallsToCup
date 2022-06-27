using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public float rotationSensitivity = 1.0f;

    void Start()
    {
        InputManager.instance.OnSwipeStarted += onRotationStarted;
        InputManager.instance.OnSwipe += onRotationMoved;
        InputManager.instance.OnSwipeEnded += onRotationEnd;
    }

    Vector3 previousPosition;

    void onRotationStarted(Vector3 startPosition)
    {
        previousPosition = startPosition;
    }

    void onRotationMoved(Vector3 currentPosition)
    {
        Vector3 delta = currentPosition - previousPosition;

        previousPosition = currentPosition;

        float aboveMidScreen = currentPosition.y > (Screen.height / 2) ? -1 : 1;

        float direction = aboveMidScreen * Mathf.Sign(Vector3.Dot(delta, transform.right));

        float rotation = (delta.magnitude / Screen.width) * direction;

        FlaskManager.instance.addRotation(rotation * rotationSensitivity);
    }
    void onRotationEnd(Vector3 currentPosition)
    {
    }
}
