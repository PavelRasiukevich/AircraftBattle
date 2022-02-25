using UnityEngine;
using Cinemachine;

public class ControllableObject : MonoBehaviour 
{
    [SerializeField] private float speed;

    private ControlsAsset _controlAsset;

    private void Awake()
    {
        _controlAsset = new ControlsAsset();
    }

    private void OnEnable()
    {
        _controlAsset.Player.Enable();
    }

    private void OnDisable()
    {
        _controlAsset.Player.Disable();
    }

    private void Start()
    {
    }

    private void Update()
    {
        transform.position += transform.forward * speed;

        var inputValue = _controlAsset.Player.Moves.ReadValue<Vector2>();

        var rotation = Quaternion.Euler(inputValue.y, 0, inputValue.x);

        transform.rotation *= rotation;
;    }
}
