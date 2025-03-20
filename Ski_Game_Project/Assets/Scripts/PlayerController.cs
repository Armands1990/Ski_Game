using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode leftInput, rightInput;
    [SerializeField] private float acceleration = 100, turnSpeed = 100,
        minSpeed = 0, maxSpeed = 500, minAcceleration = -100, maxAcceleration = 500;

    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundTransform;
    
    private float speed = 0;
    private Rigidbody rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Abs(transform.eulerAngles.y - 100);
        acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
        speed += acceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        Vector3 velocity = transform.forward * speed * Time.fixedDeltaTime;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    private float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float minRange = (newMax - newMin);
        float maxValue = (((oldValue - oldMin) / oldRange) * newRange + newMin);
        return newValue;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.Linecast(transform.position, groundTransform.position, groundLayers);
        if (isGrounded)
        {
        
            if (Input.GetKey(leftInput) && transform.eulerAngles.y <200)
            {
                transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0), Space.Self);
            }

            if (Input.GetKey(rightInput) && transform.eulerAngles.y > 91 )
            {
                transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0), Space.Self);
            }
        }
    }
}
