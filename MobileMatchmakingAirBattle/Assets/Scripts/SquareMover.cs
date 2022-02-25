using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMover : MonoBehaviour
{
    private ControlsAsset _controlAsset;

    private void Awake()
    {
        _controlAsset = new ControlsAsset();
    }

    private void OnEnable()
    {
        _controlAsset.Enable();
    }

    private void OnDisable()
    {
        _controlAsset.Disable();
    }

    private void Update()
    {
        var value = _controlAsset.Player.Moves.ReadValue<Vector2>();

        transform.position += new Vector3(value.x * 0.1f, value.y * 0.1f);
    }
}
