using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDetector : MonoBehaviour
{

    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ForeGround")
        {
            player.GetComponent<GrappleArm>().hooked = true;
            player.GetComponent<GrappleArm>().hookedObj = collision.gameObject;
        }
    }
}
