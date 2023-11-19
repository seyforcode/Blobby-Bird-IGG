using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlobbyController : MonoBehaviour
{
    public static BlobbyController instance;
    public Rigidbody rb;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float rotationSpeed = 10f;

    public UnityEvent onDeath;
    public UnityEvent<int> onScore;

    [SerializeField] private AudioClip  flapSfx;
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
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W) )
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
        AudioController.instance.PlaySFX(flapSfx);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onDeath?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score"))
        {
            onScore?.Invoke(1);
            Debug.Log("Score Added");
        }
    }
}
