using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GunController : NetworkBehaviour
{
    // Basic Requirements
    CharacterController characterController;

    // Player Gun Variables
    public int gun = 1;
    [SerializeField] public GameObject peaPrefab; // pistol gun 1 = peas
    [SerializeField] public GameObject cornPrefab; // SMG gun 2 = corn
    [SerializeField] public GameObject grainPrefab; // shotgun gun 3 = grains
    [SerializeField] public GameObject sunSeedPrefab; // sniper gun 4 = sunflower seeds
    [SerializeField] public Camera camera;
    [SerializeField] public GameObject beakPrefab;
    public Gun pistol;
    public Gun smg;
    public Gun shotgun;
    public Gun sniper;

    private void Start()
    {
        Transform beak = beakPrefab.transform;
        // Psuedo-Constructor for Gun: fields you can/should change
        // Gun(int dmg, int bps, int ammo, float dShot, float range, float spread, GameObject bullet)
        
        // PISTOL
        pistol = gameObject.AddComponent<Gun>();
        pistol.dmg = 25;
        pistol.spread = 0.02f;
        pistol.bulletPrefab = peaPrefab;
        pistol.camera = camera;
        pistol.bulletSpawn = beak;
        
        // SMG
        smg = gameObject.AddComponent<Gun>();
        smg.dmg = 10;
        smg.ammo = 20;
        smg.deltaShot = 0.3f;
        smg.bulletPrefab = cornPrefab;
        smg.camera = camera;
        smg.bulletSpawn = beak;
        
        // SHOTGUN
        shotgun = gameObject.AddComponent<Gun>();
        shotgun.dmg = 15;
        shotgun.bps = 6;
        shotgun.ammo = 12;
        shotgun.deltaShot = 2;
        shotgun.spread = 0.06f;
        shotgun.bulletPrefab = grainPrefab;
        shotgun.camera = camera;
        shotgun.bulletSpawn = beak;
        
        // SNIPER
        sniper = gameObject.AddComponent<Gun>();
        sniper.dmg = 50;
        sniper.deltaShot = 4;
        sniper.range = 100;
        sniper.bulletPrefab = sunSeedPrefab;
        sniper.camera = camera;
        sniper.bulletSpawn = beak;
    }

    // ********** Player Shooting **********
    private void Update()
    {
        // Select Gun by pressing 1, 2, 3, or 4.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gun = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gun = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gun = 4;
        }

        // Shooting
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (gun)
            {
                case 1:
                {
                    if (pistol.ready && pistol.ammo >= pistol.bps)
                    {
                        pistol.i = pistol.bps;
                        pistol.Shoot();
                    }
                    break;
                }
                case 3:
                {
                    if (shotgun.ready && shotgun.ammo >= shotgun.bps)
                    {
                        shotgun.i = shotgun.bps;
                        shotgun.Shoot();
                    }
                    break;
                }
                case 4:
                {
                    if (sniper.ready && sniper.ammo >= sniper.bps)
                    {
                        sniper.i = sniper.bps;
                        sniper.Shoot();
                    }
                    break;
                }
            }
        }
        if (gun == 2 && Input.GetKey(KeyCode.Mouse0) && smg.ready && smg.ammo >= smg.bps)
        {
            smg.i = smg.bps;
            smg.Shoot();
        }
    }
}