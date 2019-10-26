using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleArm : MonoBehaviour
{
    public GameObject arm;
    public GameObject armHolder;

    public float armTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired = false;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
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
        if (Input.GetMouseButtonDown(1) && fired == false)
        {
            fired = true;
        }

        if (fired)
        {
            rope.positionCount = 2;
            rope.SetPosition(0, armHolder.transform.position);
            rope.SetPosition(1, arm.transform.position);
        }

        if (fired == true && hooked == false)
        {
            arm.transform.Translate(Vector3.right * Time.deltaTime * armTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, arm.transform.position);

            if(currentDistance >= maxDistance)
            {
                ReturnArm();
            }
        }

        if (hooked == true && fired == true)
        {
            arm.transform.parent = hookedObj.transform;
            transform.position = Vector3.MoveTowards(transform.position, arm.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToArm = Vector3.Distance(transform.position, arm.transform.position);

            //this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            if(distanceToArm < .01)
            {
                //ReturnArm();
                if (grounded == false)
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * 1.7f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 1.8f);
                }

                StartCoroutine("Climb");
            }
        } else {
            arm.transform.parent = armHolder.transform;
            //this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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
        hooked = false;

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
