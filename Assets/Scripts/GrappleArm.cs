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

    //Values to push the player up a ledge when running into it
    public float bumpUp;
    public float bumpRight;

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
            //Create the rope vertices
            rope.positionCount = 2;
            rope.SetPosition(0, armHolder.transform.position);
            rope.SetPosition(1, arm.transform.position);
        }

        if (fired == true && grabbed == false)
        {
            //Move the arm towards the firing direction
            arm.transform.Translate(Vector3.right * Time.deltaTime * armTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, arm.transform.position);

            //If the arm does not grab something before it's max distance, return it to the body
            if(currentDistance >= maxDistance)
            {
                ReturnArm();
            }
        }

        //When the player grabs something
        if (grabbed == true && fired == true)
        {
            //The arm becomes a child of the grabbed object
            arm.transform.parent = grabbedObj.transform;
            //Move the player towards the arm
            transform.position = Vector3.MoveTowards(transform.position, arm.transform.position, Time.deltaTime * playerTravelSpeed);
            //Calculate the current distance left ot the arm
            float distanceToArm = Vector3.Distance(transform.position, arm.transform.position);

            //Make the player Kinematic so only the force of the arm affects them
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            if(distanceToArm < .2)
            {
                //ReturnArm();
                CheckIfGrounded();
                if (grounded == false)
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * bumpRight);
                    this.transform.Translate(Vector3.up * Time.deltaTime * bumpUp);
                }

                StartCoroutine("Climb");
            }
        } else {
            arm.transform.parent = armHolder.transform;
            //Go back to normal body type
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnArm();
    }

    void ReturnArm()
    {   
        //Put the arm back in the arm holder
        arm.transform.rotation = armHolder.transform.rotation;
        arm.transform.position = armHolder.transform.position;
        //Reset the variables
        fired = false;
        grabbed = false;
        //Remove the rope
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
