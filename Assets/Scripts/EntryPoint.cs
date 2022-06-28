using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        var ballComp = other.gameObject.GetComponent<Ball>();
        if (ballComp != null)
        {
            ballComp.setCollected();
        }
    }
}
