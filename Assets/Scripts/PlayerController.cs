using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    private PlayerMotor _motor;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private void Start()
    {
        this._motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Calculate our movement velocity as a 3D vector
        var xMovement = Input.GetAxisRaw(Horizontal);
        var zMovement = Input.GetAxisRaw(Vertical);

        var movHorizontal = transform.right * xMovement; // (1,0,0) OR (-1,0,0)
        var movVertical = transform.forward * zMovement; // (0,0,1) OR (0,0,-1)

        // Normalized means that total lenght is 1 no matter what.
        // final movement Vector3
        var velocity = (movHorizontal + movVertical).normalized * speed;
        
        // Apply Movement
        _motor.Move(velocity);
        
        // Calculate rotation as a 3D vector (turning around)
        var yRotation = Input.GetAxisRaw(MouseX);

        var rotation = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        
        // Apply rotation
        _motor.Rotate(rotation);
        
        
        // Calculate Camera rotation as a 3D vector (turning around)
        var xRotation = Input.GetAxisRaw(MouseY);

        var cameraRotation = new Vector3(xRotation, 0f , 0f) * lookSensitivity;
        
        // Apply rotation
        _motor.RotateCamera(cameraRotation);

    }
}
