using UnityEngine;
using V2 = UnityEngine.Vector2;
using V3 = UnityEngine.Vector3;

public class EditorExposer : MonoBehaviour
{
    [SerializeField] private V3 _vector3;
    [SerializeField] private V2 _vector2;
    [SerializeField] private Quaternion _quaternion;
    [SerializeField] private int _int;

    private V3 _position;
    private Quaternion _rotation;
    private Transform _selfTransform;

    private void Start()
    {
        _selfTransform = this.transform;

        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _position = _selfTransform.position;
        _rotation = _selfTransform.rotation;
        _rotation = _selfTransform.rotation;
        _rotation = _selfTransform.rotation;
        _rotation = _selfTransform.rotation;
        _rotation = _selfTransform.rotation;
        _rotation = _selfTransform.rotation;
        _rotation = _selfTransform.rotation;

        Debug.Log("Start", this);
    }
}
