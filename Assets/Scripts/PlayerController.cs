using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private PlayerMotor _motor;
    private string Horizontal = "Horizontal";
    private string Vertical = "Vertical";

    private void Start()
    {
        this._motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Calculate our movement velocity as a 3D vector
        float xMovement = Input.GetAxisRaw(Horizontal);
        float zMovement = Input.GetAxisRaw(Vertical);

        Vector3 movHorizontal = transform.right * xMovement; // (1,0,0) OR (-1,0,0)
        Vector3 movVertical = transform.forward * zMovement; // (0,0,1) OR (0,0,-1)

        // Normalized means that total lenght is 1 no matter what.
        // final movement Vector3
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        
        // Apply Movement
        // _motor.Move(velocity);

    }
}
