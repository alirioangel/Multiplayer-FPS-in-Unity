using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask mask;
    private const string ShootButton = "Fire1";

    private void Start()
    {
        if (camera == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown(ShootButton))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position,
            camera.transform.forward,
            out hit,
            weapon.range,
            mask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                CmdPlayerShoot(hit.collider.name);
            }
        }
    }

    [Command]
    void CmdPlayerShoot(string _ID)
    {
        Debug.Log(_ID+ " has been shoot");
    }
}
