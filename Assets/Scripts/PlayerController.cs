using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float _thrusterForce = 1000f;

    [Header("Spring Settings")] 
    [SerializeField] private float _jointSpring = 7f;
    [SerializeField] private float _jointDamper = 2f;
    [SerializeField] private float _joinMaxForce = 3.402823e+38f;
    private PlayerMotor _motor;
    private ConfigurableJoint _joint;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const string Jump = "Jump";

    private void Start()
    {
        this._motor = GetComponent<PlayerMotor>();
        this._joint = GetComponent<ConfigurableJoint>();
        SetJoinSettings(_jointSpring);
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

        var cameraRotationX = xRotation  * lookSensitivity;
        
        // Apply rotation
        _motor.RotateCamera(cameraRotationX);

        var thrusterForce = Vector3.zero;
        // Apply thrusterForce
        if (Input.GetButton(Jump))
        {
            thrusterForce = Vector3.up * _thrusterForce;
            SetJoinSettings(0f);
        }
        else
        {
            SetJoinSettings(_jointSpring);
        }

        _motor.ApplyThruster(thrusterForce);
        
    }

    private void SetJoinSettings(float jointSpring)
    {
        _joint.connectedAnchor = new Vector3(0f, 1.3f, 0f);
        _joint.targetPosition = new Vector3(0f, -0.1f, 0f);
        _joint.yDrive = new JointDrive
        {
            positionDamper = _jointDamper, 
            positionSpring = jointSpring, 
            maximumForce = _joinMaxForce
        };
    }
}
