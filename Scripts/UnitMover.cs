using UnityEngine;
using System.Collections;
using System;

public class UnitMover : MonoBehaviour
{

    [SerializeField] private float _initialSpeed;

    private float _speed;
    private Transform _transform;

    public void Start()
    {
        _transform = transform;
        _speed = _initialSpeed;
    }

    public void Move(Vector3 direction)
    {
        _speed = _initialSpeed;
        Vector3 offset = direction.normalized * _speed * Time.deltaTime;

        _transform.Translate(offset);
    }

    public void StopMoving()
    {
        _speed = 0;
    }
}