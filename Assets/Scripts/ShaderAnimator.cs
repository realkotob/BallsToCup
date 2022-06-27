using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderAnimator : MonoBehaviour
{
    public float value = 0.0f;
    private float previousValue = 0.0f;
    public string propertyName = "_Value";
    void Start()
    {
    }

    void Update()
    {
        if (value != previousValue)
        {
            previousValue = value;
            Shader.SetGlobalFloat(propertyName, value);
            // GetComponent<Image>().sharedMaterial.SetFloat(propertyName, value);
        }
    }
}
