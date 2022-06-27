using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationStopper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // on trigger
    void OnTriggerEnter(Collider other)
    {
        var ballComp = other.gameObject.GetComponent<Ball>();
        if (ballComp != null)
        {
            ballComp.toggleValidation(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var ballComp = other.gameObject.GetComponent<Ball>();
        if (ballComp != null)
        {
            ballComp.checkIfInContainer();
        }
    }
}
