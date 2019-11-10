﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMode : MonoBehaviour
{    
    //Floats for keeping track of old and new scales
    public Vector3 origScale;
    public Vector3 largeScale = new Vector3(10f, 10f, 10f);
    //Floats for keeping track of new and old speeds
    public float oldSpeed;
    public float slowSpeed;
    
    public MovementScript moveScript;

    // Start is called before the first frame update
    void Start()
    {
        origScale = this.transform.localScale;
        moveScript = this.GetComponent<MovementScript>();
        //this.transform.localScale = largeScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Fire3") > 0){
            this.transform.position += new Vector3(0f, 0.5f, 0f);
            this.transform.localScale = largeScale;
            oldSpeed = moveScript.speed;
            moveScript.speed = slowSpeed;
        }
    }
}
