using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterScript : MonoBehaviour
{
    public GameObject shooter;
    public Rigidbody2D shooterRigid;
    private SpringJoint2D shooterSpring;

    Vector3 shootDirection;

    public float shootPower = 5;
    public float frequencyForce = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ForeGround"))
        {
            //freeze the arm on the wall
            shooterRigid.constraints = RigidbodyConstraints2D.FreezeAll;

            //set spring frequency to however fast we want the player to follow its arm
            shooterSpring = shooter.GetComponent<SpringJoint2D>();
            shooterSpring.frequency = frequencyForce;

        }

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //let the arm move
            shooterRigid.isKinematic = false;

            //get the direction the mouse is in, apply force to throw along that direction
            shootDirection = Input.mousePosition;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;

            shooterRigid.AddForce(shootDirection * shootPower, ForceMode2D.Impulse);
        }
    }

}
