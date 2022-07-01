using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : GenericSingleton<Mug>
{
    public Animator mugGlowVfx;
    void Start()
    {
        
    }

    public void playMugGlow(){
        mugGlowVfx.gameObject.SetActive(true);
    }

   
}
