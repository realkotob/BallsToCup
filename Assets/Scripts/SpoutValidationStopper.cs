using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoutValidationStopper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        var ballComp = other.gameObject.GetComponent<Ball>();
        if (ballComp != null)
        {
            ballComp.toggleSpoutValidation(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var ballComp = other.gameObject.GetComponent<Ball>();
        if (ballComp != null)
        {
            ballComp.checkIfInFlaskSpout();
        }
    }
}
