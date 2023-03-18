using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("====Reference====")]
    [SerializeField] Rigidbody _rigidbody;


    [Space(20)]
    [Header("====Settings====")]
    [Range(0, 10)]
    [SerializeField] float _speed;



    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.up * _speed;
    }
}
