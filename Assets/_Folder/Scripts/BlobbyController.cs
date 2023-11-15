using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlobbyController : MonoBehaviour
{

    public static BlobbyController instance;
    
    private Rigidbody rb;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float rotationSpeed = 10f;

    public UnityEvent onDeath;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W))
        {
            Flap();
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0 ,rb.velocity.y * 10f), Time.deltaTime * rotationSpeed);
    }

    private void Flap()
    {
        rb.velocity = Vector3.up *_speed  ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onDeath?.Invoke();
        }
    }
}
