using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeWithForce : MonoBehaviour
{
    private Rigidbody _rb;
    private ControlsAsset _cAsset;

    private Vector2 _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _cAsset = new ControlsAsset();
    }

    private void OnEnable()
    {
        _cAsset.Enable();
    }

    private void OnDisable()
    {
        _cAsset.Disable();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _input = _cAsset.Player.WithForce.ReadValue<Vector2>();

        var spaceInput = _cAsset.Player.Push.ReadValue<float>();

       
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * 10;

        var rotation = Quaternion.AngleAxis(45, Vector3.up);

        rotation.ToAngleAxis(out float angle, out Vector3 axis);

        _rb.angularVelocity = angle * axis * Mathf.Deg2Rad * _input.x;

    }
}
