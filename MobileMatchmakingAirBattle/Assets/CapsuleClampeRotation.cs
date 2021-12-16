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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var comp = GetComponent<Rigidbody>();
            comp.AddRelativeTorque(Vector3.forward * -1 , ForceMode.Impulse);
        }
        Debug.Log(transform.rotation.eulerAngles.z);

        if (_rigid.rotation.eulerAngles.z >= 45 && _rigid.rotation.eulerAngles.z <= 315)
        {
            var angVelZ = _rigid.angularVelocity;
            angVelZ.z = 0;

            _rigid.angularVelocity = angVelZ;

        }
    }
}
