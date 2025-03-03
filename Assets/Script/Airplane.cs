using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float enginePower = 20f;
    [SerializeField] private float liftBooster = 0.5f;
    [SerializeField] private float drag = 0.001f;
    [SerializeField] private float angularDrag = 0.001f;
    [SerializeField] private float yawPower = 50f;
    [SerializeField] private float pitchPower = 50f;
    [SerializeField] private float rollPower = 30f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // ตั้งค่า Drag และ Angular Drag เริ่มต้น
        rb.drag = drag;
        rb.angularDrag = angularDrag;
    }

    void FixedUpdate()
    {
        float yaw = Input.GetAxis("Horizontal") * yawPower;
        float pitch = Input.GetAxis("Vertical") * pitchPower;
        float roll = 0f;

        // ใช้ Q และ E แทนการหมุน Roll
        if (Input.GetKey(KeyCode.Q)) roll = rollPower;
        if (Input.GetKey(KeyCode.E)) roll = -rollPower;

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * enginePower, ForceMode.Force);

            // คำนวณแรงยก (Lift)
            Vector3 lift = Vector3.Project(rb.velocity, transform.forward);
            rb.AddForce(transform.up * lift.magnitude * liftBooster, ForceMode.Force);

            // ใช้ Torque ควบคุมการหมุน
            rb.AddTorque(transform.up * yaw, ForceMode.Force);
            rb.AddTorque(transform.right * pitch, ForceMode.Force);
            rb.AddTorque(transform.forward * roll, ForceMode.Force);
        }
    }
}