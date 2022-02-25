using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackerBall : MonoBehaviour
{
    [SerializeField] private float power;

    private ControlsAsset _ca;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _ca = new ControlsAsset();
        _ca.Player.Push.performed += PushHandler;
    }

    private void PushHandler(InputAction.CallbackContext obj)
    {
        _rb.AddForce(Vector3.forward * power, ForceMode.Force);
    }

    private void OnEnable()
    {
        _ca.Enable();
    }

    private void OnDisable()
    {
        _ca.Disable();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
