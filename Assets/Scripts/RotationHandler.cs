using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField]
    private float rotationSensitivity = 10f;

    // [SerializeField]
    // private float maxRotationPerFrame = 0.1f;

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

        // get angle to center of screen
        var centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 delta = currentPosition - centerOfScreen;
        Vector3 delta_prev = previousPosition - centerOfScreen;
        float angle = -Vector3.Angle(delta, delta_prev);
        if (Vector3.Cross(delta, delta_prev).z < 0)
        {
            angle = -angle;
        }

        // rotate object
        var target_transform = FlaskManager.instance.transform;

        // divide angle by distance to center of screen
        float distance = (currentPosition - centerOfScreen).magnitude;
        float normalize_to_distance = distance / Screen.width;
        angle = angle * normalize_to_distance * rotationSensitivity;

        var finalRotation = angle * Time.deltaTime * 60;
        // if(Mathf.Abs(finalRotation) > maxRotationPerFrame){
        //     finalRotation = maxRotationPerFrame * Mathf.Sign(finalRotation);
        // }

        target_transform.Rotate(Vector3.forward, finalRotation);
        previousPosition = currentPosition;
    }


    void onRotationEnd(Vector3 currentPosition)
    {
    }

}
