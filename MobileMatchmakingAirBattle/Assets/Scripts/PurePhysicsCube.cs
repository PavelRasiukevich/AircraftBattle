using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurePhysicsCube : MonoBehaviour
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
        _rb.velocity = _rb.transform.forward * 0.5f;

        var rotationVector = new Vector3(_input.y * 45, _input.x * 45);

        _rb.angularVelocity = rotationVector * Mathf.Deg2Rad;

    }
}
