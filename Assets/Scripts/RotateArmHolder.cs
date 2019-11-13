﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArmHolder : MonoBehaviour
{
    //public GameObject top;
    //private Vector3 lookPos;
    public GrappleArm arm;

    public float posPushRight;
    public float negPushRight;

    // Start is called before the first frame update
    void Start()
    {
        arm = GetComponentInParent<GrappleArm>();
        //Get the value for pushing right, and make a negative version
        posPushRight = arm.bumpRight;
        negPushRight -= posPushRight;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //See if the player has their arm aimed left and set the push accordingly
        if (this.transform.rotation.eulerAngles.z > 90f && this.transform.rotation.eulerAngles.z < 270f){
            arm.bumpRight = negPushRight;
        } else {
            arm.bumpRight = posPushRight;
        }
    }
}
