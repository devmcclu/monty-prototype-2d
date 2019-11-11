using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            LargeMode playerLarge = other.GetComponent<LargeMode>();
            if (playerLarge.isLarge == false){
                playerLarge.BecomeLarge();
            }
        }
    }

    
}
