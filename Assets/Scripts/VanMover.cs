using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanMover : MonoBehaviour
{
    private float speed = 8.0f;
    private bool isBlocked = false;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!isBlocked)
        {
            // Forward movement
            transform.position += -Vector3.forward * speed * Time.deltaTime;
        }

        // Left-right movement
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x > -3.5f)
            {
                transform.position += -Vector3.right * speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x < 3.5f)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }

        // If blocked, check if player has moved away from obstacle
        if (isBlocked)
        {
            float distanceFromCollision = Vector3.Distance(transform.position, lastPosition);
            if (distanceFromCollision > 1.0f)
            {
                isBlocked = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crate"))
        {
            // Store position where collision occurred
            lastPosition = transform.position;
            isBlocked = true;
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(-1); // Deduct 1 point
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(1); // Add 1 point
            }
            Destroy(other.gameObject);
        }
    }
}
