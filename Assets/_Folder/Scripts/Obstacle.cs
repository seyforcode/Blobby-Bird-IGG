using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private void Update()
    {
        transform.position += Vector3.left * ( _speed * Time.deltaTime );
    }
}
