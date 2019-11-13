using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArmHolder : MonoBehaviour
{
    //public GameObject top;
    //private Vector3 lookPos;
    public GrappleArm arm;

    public float posPushRight;
    public float posPushUp;
    public float negPushRight;
    public float negPushUp;

    // Start is called before the first frame update
    void Start()
    {
        arm = GetComponentInParent<GrappleArm>();
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

        if (this.transform.rotation.z > 90f || this.transform.rotation.z < -90f){
            arm.bumpRight = negPushRight;
        } else {
            arm.bumpRight = posPushRight;
        }
    }
}
