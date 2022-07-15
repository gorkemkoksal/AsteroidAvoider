using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroids : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
  
        if (playerHealth != null)
        {          
            playerHealth.Crush();
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
