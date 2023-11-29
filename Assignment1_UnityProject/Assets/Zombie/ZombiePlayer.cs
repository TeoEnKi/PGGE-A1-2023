using PGGE;
using PGGE.Patterns;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class ZombiePlayer : MonoBehaviour
{
    //can replace with animationclip.length
    public float attackTime = 10;
    public float currentTime = 0;
    public int counter = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("o"))
        {
            if(currentTime < attackTime)
            {
                currentTime += 1*Time.deltaTime;
            }
            else
            {
                counter++;
                Debug.Log("animation run time has ended, adding to counter. The counter is:" + counter);
                currentTime = 0;
            }
        }
    }


}
