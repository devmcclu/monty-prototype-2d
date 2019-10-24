using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchScript : MonoBehaviour
{
    public Rigidbody2D parentRigid;
    public float slingThrust = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        slingCheck();
    }

    void slingCheck()
    {
        //gets the mouse position
        Vector3 mousePos = Input.mousePosition;

        //converts it to be usable
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(
            //this creates the line between the player and the mouse that i want to apply the force to
            mousePos.x - transform.position.x, 
            mousePos.y - transform.position.y);

        //make the top of the sprite look in the direction we want
        transform.up = direction;

        //check if left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            //get local scale of y on the player gameobject
            var scaleY = transform.localScale.y;
            //scale it to the mouse. magnitude gets the length of the line between mouse & player. 
            scaleY = direction.magnitude * 10;

            //create variable that holds the local scale of player
            var scaleYUpdate = this.transform.localScale;

            //modify it to updated value
            scaleYUpdate = new Vector3(1f, scaleY);

            transform.localScale = scaleYUpdate;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //var for maximum sling strength
            var maxSling = direction.magnitude;
           
            //if over capped value, set to capped value
            if (maxSling > 3.7f)
            {
                maxSling = 3.7f;
                
                //set scale back to 1 before launching
                this.transform.localScale = new Vector3(1f, 1f, 1f);

                //add force to rigid along the line between player & mouse, use capped value for magnitude
                parentRigid.AddForce(-direction.normalized * (slingThrust * maxSling));
            }
            else
            {
                this.transform.localScale = new Vector3(1f, 1f, 1f);

                //add force to rigid along line between player & mouse, use dynamic value for magnitude
                parentRigid.AddForce(-direction.normalized * (slingThrust * direction.magnitude));
            }

        }

    }

}
