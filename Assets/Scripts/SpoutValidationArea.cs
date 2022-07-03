using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoutValidationArea : MonoBehaviour
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
            ballComp.setInsideSpout(true);
            ballComp.toggleSpoutValidation(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var ballComp = other.gameObject.GetComponent<Ball>();
        if (ballComp != null)
        {
            ballComp.setInsideSpout(false);
        }
    }
}
