using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConsumeBall : MonoBehaviour
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
            ballComp.setConsumed();
        }
    }
}
