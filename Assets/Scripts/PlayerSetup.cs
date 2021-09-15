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

   [SerializeField] private string remoteLayerName= "RemotePlayer";

   private Camera _sceneCamera;

   private void Start()
   {
      if (!isLocalPlayer)
      {
         DisableComponents();
         AssignRemoteLayer();
      }
      else
      {
         _sceneCamera = Camera.main;
         if (_sceneCamera != null) _sceneCamera.gameObject.SetActive(false);
      }
      
     RegisterPlayer();
   }

   private void RegisterPlayer()
   {
      var _ID = "Player "+ GetComponent<NetworkIdentity>().netId;
      transform.name = _ID;
   }

   private void DisableComponents()
   {
      foreach (var tBehaviour in componentsToDisable)
      {
         tBehaviour.enabled = false;
      }   
   }

   private void AssignRemoteLayer()
   {
      gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
   }

   private void OnDisable()
   {
      if (_sceneCamera != null)
      {
         _sceneCamera.gameObject.SetActive(true);
      }
   }
}
