using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    [SerializeField] private float spin = 2000;
    private Rigidbody2D rb;

    private float angle;
    private float rotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.angularVelocity = spin*Time.deltaTime;
    }

    private void Rotation()
    {
        angle = transform.rotation.eulerAngles.z;
        angle += rotationSpeed * Time.deltaTime;  //Tăng vị trí quay
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Quay tròn
    }
}
