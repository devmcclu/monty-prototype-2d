using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public float jumpForce = 10;
    public Rigidbody2D playerRigid;

    public BoxCollider2D playerCol;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Debug.Log("hello");
            playerRigid.AddForce(Vector2.up * jumpForce);
        }

        Debug.Log(isGrounded);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

}
