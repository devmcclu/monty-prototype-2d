using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy_Belly : MonoBehaviour
{
    public Rigidbody2D playerRigid;
    public PhysicsMaterial2D bounceMat;
    public PhysicsMaterial2D currentMat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            Bounce_Belly();
        }

    }

    void Bounce_Belly()
    {

        playerRigid.AddForce(Vector2.up * 450);
        transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, -90f);

        playerRigid.sharedMaterial = bounceMat;

        StartCoroutine("BounceTimer");
        
    }

    IEnumerator BounceTimer()
    {
        yield return new WaitForSeconds(6);

        playerRigid.sharedMaterial = currentMat;

        transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 0f);
    }
}
