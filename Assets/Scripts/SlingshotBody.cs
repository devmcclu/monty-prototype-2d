using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBody : MonoBehaviour
{
    //Object the player has grabbed
    public GameObject grabbedObj;
    //The player's body
    public GameObject body;

    public GameObject bodyHolder;
    public MovementScript movement;

    //Speed at which the player can walk when holding something
    public float holdSpeed;
    public float orgSpeed;

    public bool grabbed = false;
    //Check if the player is near a grabable item
    public bool nearGrabable = false;

    //Representation of the player's body when stretching
    public LineRenderer stretchedBody;
    
    //How far the body can go
    public float maxDistance;
    //How far away from the player the body is
    private float currentDistance;

    //
    public float slingThrust;

    // Start is called before the first frame update
    void Start()
    {   
        movement = this.GetComponent<MovementScript>();
        orgSpeed = movement.speed;
        stretchedBody = body.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //firing the hook
        if (Input.GetAxis("Fire1") > 0 && grabbed == false && nearGrabable == true)
        {
            grabbed = true;
            this.GetComponent<MovementScript>().speed = holdSpeed;

            //Create the stretchedBody vertices
            stretchedBody.positionCount = 2;
            stretchedBody.SetPosition(0, bodyHolder.transform.position);
            stretchedBody.SetPosition(1, body.transform.position);

            body.transform.parent = grabbedObj.transform;
        }

        if (grabbed == true)
        {
            stretchedBody.SetPosition(0, bodyHolder.transform.position);
            stretchedBody.SetPosition(1, body.transform.position);
            //Move the arm towards the firing direction
            //arm.transform.Translate(Vector3.right * Time.deltaTime * armTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, body.transform.position);

            //If the player moves too far, stop the player from moving
            if(currentDistance >= maxDistance)
            {
                holdSpeed = 0;
                movement.speed = holdSpeed;
            }
        }

        if (Input.GetAxis("Fire1") == 0 && grabbed == true)
        {
            FlingBody();
            movement.speed = orgSpeed;
        }
    }

    void FlingBody()
    {   
        Vector2 direction = new Vector2(
                                body.transform.position.x - bodyHolder.transform.position.x,
                                body.transform.position.y - bodyHolder.transform.position.y);

        this.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * (slingThrust * direction.magnitude));
        body.transform.rotation = bodyHolder.transform.rotation;
        body.transform.position = bodyHolder.transform.position;
        //Put the body back in the body holder
        body.transform.parent = bodyHolder.transform;
        //Reset the variables
        grabbed = false;
        //Remove the rope
        stretchedBody.positionCount = 0;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ForeGround")
        {
            nearGrabable = true;
            grabbedObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ForeGround")
        {
            nearGrabable = false;
            grabbedObj = collision.gameObject;
        }
    }
}
