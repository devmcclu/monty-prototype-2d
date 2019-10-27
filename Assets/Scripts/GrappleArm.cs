using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleArm : MonoBehaviour
{
    //The Arm of the player
    public GameObject arm;
    //Empty game object where arm always goes back to
    public GameObject armHolder;

    //How fast the arm moves
    public float armTravelSpeed;
    //How fast the player moves when hooked
    public float playerTravelSpeed;

    public static bool fired = false;      
    //If the player has grabbed something
    public bool grabbed;
    //What the player has grabbed
    public GameObject grabbedObj;

    //How far the arm can go
    public float maxDistance;
    //How far away from the armHolder the arm is
    private float currentDistance;

    private bool grounded;

    public LineRenderer rope;

    private void Start()
    {
        rope = arm.GetComponent<LineRenderer>();
    }


    private void Update()
    {
        //firing the hook
        if (Input.GetAxis("Fire2") > 0 && fired == false)
        {
            fired = true;
        }

        if (fired)
        {
            rope.positionCount = 2;
            rope.SetPosition(0, armHolder.transform.position);
            rope.SetPosition(1, arm.transform.position);
        }

        if (fired == true && grabbed == false)
        {
            arm.transform.Translate(Vector3.right * Time.deltaTime * armTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, arm.transform.position);

            if(currentDistance >= maxDistance)
            {
                ReturnArm();
            }
        }

        if (grabbed == true && fired == true)
        {
            arm.transform.parent = grabbedObj.transform;
            transform.position = Vector3.MoveTowards(transform.position, arm.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToArm = Vector3.Distance(transform.position, arm.transform.position);

            if(distanceToArm < .01)
            {
                //ReturnArm();
                CheckIfGrounded();
                if (grounded == false)
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * 1.7f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 1.8f);
                }

                StartCoroutine("Climb");
            }
        } else {
            arm.transform.parent = armHolder.transform;
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnArm();
    }

    void ReturnArm()
    {
        arm.transform.rotation = armHolder.transform.rotation;
        arm.transform.position = armHolder.transform.position;
        fired = false;
        grabbed = false;

        rope.positionCount = 0;
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = .1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        } else {
            grounded = false;
        }
    }
}
