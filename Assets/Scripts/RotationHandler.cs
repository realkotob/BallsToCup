using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    [Range(0.01f, 5000f)]
    [SerializeField]
    private float rotationSensitivity = 10f;

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
        float distance_prev = (previousPosition - centerOfScreen).magnitude;
        float distance_ratio = distance / distance_prev;
        // float normalize_to_distance = Mathf.Clamp(Screen.width / distance, 0f, 1f);
        float arc_length = Mathf.Abs(angle) * Mathf.PI * Screen.width;
        float normalize_to_distance = distance / Screen.width;
        angle = angle * normalize_to_distance * rotationSensitivity;

        target_transform.Rotate(Vector3.forward, angle * Time.deltaTime * 60);
        previousPosition = currentPosition;
    }

    // void onRotationMoved(Vector3 currentPosition)
    // {

    //     Vector3 delta = currentPosition - previousPosition;

    //     float aboveMidScreen = previousPosition.y > (Screen.height / 2) ? -1 : 1;
    //     previousPosition = currentPosition;

    //     float direction = aboveMidScreen * Mathf.Sign(Vector3.Dot(delta, transform.right));

    //     float rotation = 180 * (delta.magnitude / Screen.width) * direction;

    //     FlaskManager.instance.addRotation(rotation * rotationSensitivity * Time.deltaTime);
    // }

    void onRotationEnd(Vector3 currentPosition)
    {
    }

    // private Vector2 startingPosition;

    // void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         var target_transform = FlaskManager.instance.transform;
    //         switch (touch.phase)
    //         {
    //         case TouchPhase.Began:
    //             startingPosition = touch.position;
    //             break;
    //         case TouchPhase.Moved:
    //             var delta = touch.deltaPosition;
    //             // if (startingPosition < touch.position.x)
    //             // {
    //             //     target_transform.Rotate(Vector3.back, -rotationSensitivity * Time.deltaTime *
    //             delta.magnitude);
    //             // }
    //             // else if (startingPosition > touch.position.x)
    //             // {
    //             //     target_transform.Rotate(Vector3.back, rotationSensitivity * Time.deltaTime * delta.magnitude);
    //             // }
    //             var angle = angleBetweenPoints(touch.position, startingPosition);
    //             target_transform.Rotate(Vector3.back, angle * rotationSensitivity * Time.deltaTime *
    //             delta.magnitude); break;
    //         case TouchPhase.Ended:
    //             // Debug.Log("Touch Phase Ended.");
    //             break;
    //         case TouchPhase.Stationary:
    //             startingPosition = touch.position;
    //             break;
    //         }
    //     }
    // }

    float angleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
