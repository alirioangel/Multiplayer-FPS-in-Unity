using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class PlayerSetup : NetworkBehaviour
{
   [SerializeField]
   private Behaviour[] componentsToDisable;

   private Camera _sceneCamera;

   private void Start()
   {
      if (!isLocalPlayer)
      {
         foreach (var tBehaviour in componentsToDisable)
         {
            tBehaviour.enabled = false;
         }   
      }
      else
      {
         _sceneCamera = Camera.main;
         if (_sceneCamera != null) _sceneCamera.gameObject.SetActive(false);
      }
      
   }

   private void OnDisable()
   {
      if (_sceneCamera != null)
      {
         _sceneCamera.gameObject.SetActive(true);
      }
   }
}
