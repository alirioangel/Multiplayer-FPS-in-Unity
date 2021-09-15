
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
   private Vector3 _velocity = Vector3.zero;
   private Vector3 _rotation = Vector3.zero;
   private float _rotationCameraX = 0f;
   private float _currentCameraRotation = 0f;
   private Vector3 _thrusterForce = Vector3.zero;

   [SerializeField] private Camera camera;
   [SerializeField] private float cameraRotationLimit = 85f;
  
   private Rigidbody _rigidbody;

   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody>();
   }

   /**
    * @Param velocity - Gets a movement vector3
    */
   public void Move(Vector3 velocity)
   {
      _velocity = velocity;
      
   }

   /**
    * @Param rotation - Gets a rotation vector3
    */
   public void Rotate(Vector3 rotation)
   {
      _rotation = rotation;
   }  
   
   /**
    * @Param rotationCamera - Gets a rotation camera vector3
    */
   public void RotateCamera(float rotationCameraX)
   {
      _rotationCameraX = rotationCameraX;
   }
   
   
   public void ApplyThruster(Vector3 thrusterForce)
   {
      _thrusterForce = thrusterForce;
   }
   

   // Fixed Update runs every physics iteration
   private void FixedUpdate()
   {
      PerformMovement();
      PerformRotation();
   }
   
   // Perform movement based on velocity Variable
   private void PerformMovement()
   {
      if (_velocity != Vector3.zero)
      {
         /* it's different of transform.translate because this one actually stop the
          * rigidbody from moving there if collides with something on the way.
          * it does all of the physics checks for us.
          */
         _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
      }

      if (_thrusterForce != Vector3.zero)
      {
         _rigidbody.AddForce(_thrusterForce, ForceMode.Acceleration);
      }
      
   }

   private void PerformRotation()
   {
      /*
       * Quaternion helps to do awesome things without knows about how Quaternion calculation
       * is. xD
       */
      _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));

      if (camera != null)
      {
         // Set our Rotation and Clamp it
         _currentCameraRotation -= _rotationCameraX;
         _currentCameraRotation = Mathf.Clamp(_currentCameraRotation,
            -cameraRotationLimit, cameraRotationLimit);
            
         // Apply our rotation to the transform of our camera.
         camera.transform.localEulerAngles = new Vector3(_currentCameraRotation, 0f, 0f);
      }
   }

}