using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBody : MonoBehaviour
{
    //Object the player has grabbed
    public GameObject grabbedObject;
    //The player's body
    public GameObject body;

    //Speed at which the player can walk when holding something
    public float holdSpeed;

    //Representation of the player's body when stretching
    public LineRenderer stretchedBody;

    
    //How far the body can go
    public float maxDistance;
    //How far away from the player the body is
    private float currentDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
