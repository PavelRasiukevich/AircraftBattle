using UnityEngine;

public class CapsuleClampeRotation : MonoBehaviour
{
    private Rigidbody _rigid;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var angle = Quaternion.AngleAxis(1.0f, new Vector3(Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal") ));
        var rot = new Vector3(angle.x, angle.y, angle.z);
        _rigid.AddTorque(rot, ForceMode.VelocityChange);
    }
}
