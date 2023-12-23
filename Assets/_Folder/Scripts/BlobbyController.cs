using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlobbyController : MonoBehaviour
{
    public static BlobbyController instance;
    

    [Header("Mesh")]
    [SerializeField] private MeshRenderer meshRenderer;
    
    [Header("Movement")]
    public BoxCollider boxCollider;
    public Rigidbody rb;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float rotationSpeed = 10f;

    [HideInInspector] public UnityEvent onDeath;
    [HideInInspector] public UnityEvent<int> onScore;

    [Header("VFX")]
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject jumpVFX;
    
    [Header("SFX")]
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
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W) && UIController.gameStarted )
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
        if (UIController.gameStarted)
        {
            var jumpFx = Instantiate(jumpVFX,new Vector3(transform.position.x,transform.position.y - 1f,transform.position.z),Quaternion.identity);
            Destroy(jumpFx,.5f);
            AudioController.instance.PlaySFX(flapSfx);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && UIController.gameStarted)
        {
            onDeath?.Invoke(); 
            CloseBird();
            var deathVfx = Instantiate(deathVFX,transform.position, Quaternion.identity);
            Destroy(deathVfx,1f);
        }
    }
    
    public void OpenBird()
    {
        boxCollider.isTrigger = false;
        rb.isKinematic = false;
        meshRenderer.enabled = true;
    }

    private void CloseBird()
    {
        boxCollider.isTrigger = true;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score") && UIController.gameStarted) 
        {
            onScore?.Invoke(1);
            Debug.Log("Score Added");
        }
    }
}
