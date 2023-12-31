using System.Collections;
using System.Collections.Generic;
using Mirror.Examples.Additive;
using TMPro;
using Mirror;
using UnityEngine;

// Note: for simplicity, comments and variables will refer to weapons as "guns" and "bullets"
// The concept behind our weapons is that they can be used the same as FPS, however:
// The "weapons" are just seeds the chickens spit. Therefore:
// There is no need to reload, or have separate gun models.
// Different foods act differently when spit, though, and those are different "weapons".
// This script is a general template for all types of "guns".
// Referenced Code: https://www.dropbox.com/s/g077hpelcdnjyqm/GunSystem.cs?dl=0
public class Gun : NetworkBehaviour
{
    // gun damage, bullets generated per shot, and ammunition; counter i
    public int dmg, bps, ammo, i;
    // time between two shots/trigger pulls, range of bullet, spread/inaccuracy in aiming
    public float deltaShot, range, spread;
    // whether the gun is ready for next shot or not
    public bool ready = true;
    // other
    [SerializeField]
    public TextMeshProUGUI text;
    public GameObject bulletPrefab;
    public Transform bulletSpawn; // set to chicken's beak
    public Camera cam;
    public Team enemy;

    // Constructor for simple gun generation
    public Gun()
    {
        bps = 1;
        ammo = 10;
        range = 50;
        spread = 0.01f;
        deltaShot = 1;
    }

    public void Shoot()
    {
        ready = false;
        // send out a raycast based on camera, factoring in gun spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = cam.transform.forward + new Vector3(x, y, 0);
        // create a bullet
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.dmg = dmg;
        bulletController.target = cam.transform.position + direction * range;
        bulletController.enemy = enemy;
        ammo--;
        i--;

        Invoke("Delay", deltaShot);
        if (i > 0 && ammo > 0)
        {
            Invoke("Shoot", 0);
        }
    }

    public void Delay()
    {
        ready = true;
    }
}
