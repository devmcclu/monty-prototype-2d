using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMode : MonoBehaviour
{
    public Vector3 origScale;
    public Vector3 largeScale = new Vector3(10f, 10f, 10f);

    // Start is called before the first frame update
    void Start()
    {
        origScale = this.transform.localScale;
        //this.transform.localScale = largeScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
