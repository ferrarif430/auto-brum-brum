using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController2D : MonoBehaviour
{
    public float accelerationForce = 5f;
    public float maxSpeed = 50f;
    public float rotationSpeed = 125f;
    public float friction = 2f; 
    public float brakeForce = 10f;
    public float lateralFrictionFactor = 2f;  

    private Rigidbody2D rigidbody;
    private float moveForward;
    private float rotate;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveForward = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (Mathf.Approximately(moveForward, 0))
        {
            rigidbody.drag = friction;
        }
        else
        {
            rigidbody.drag = 0;
        }

        Vector2 lateralVelocity = Vector2.Dot(rigidbody.velocity, transform.right) * transform.right;
        rigidbody.velocity -= lateralVelocity * lateralFrictionFactor * Time.fixedDeltaTime; 
        
        if (moveForward > 0)
        {
            Vector2 forceDirection = transform.up * moveForward;
            rigidbody.AddForce(forceDirection * accelerationForce, ForceMode2D.Force);
        }
        
        else if (moveForward < 0)
        {
            Vector2 brakeDirection = -rigidbody.velocity.normalized;
            rigidbody.AddForce(brakeDirection * brakeForce, ForceMode2D.Force);
        }

        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }

        float rotationAmount = rotate * rotationSpeed * Time.fixedDeltaTime;
        float newRotation = rigidbody.rotation - rotationAmount;
        rigidbody.MoveRotation(newRotation);
    }
}