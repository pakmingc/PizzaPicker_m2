using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void Start()
    {
        // Set up the collider based on the object's tag
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            // Add a box collider if there isn't one
            col = gameObject.AddComponent<BoxCollider>();
        }

        if (gameObject.CompareTag("Pizza"))
        {
            col.isTrigger = true;
        }
        else if (gameObject.CompareTag("Crate"))
        {
            col.isTrigger = false;
        }

        // Add Rigidbody if it doesn't exist
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Make it kinematic so it doesn't fall
            rb.useGravity = false; // Disable gravity
        }
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // This method will handle collision-based pickups if needed
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Logic for when the player picks up the item
            Destroy(gameObject); // Destroy the pickup item after collection
        }
    }
}



