using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_2 : WeaponBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        base.Shoot();
        Bullet();
    }

    private void Bullet()
    {
        GameObject bullet = Instantiate(weaponSO.bulletPrefab, pointShoot1.position, Quaternion.identity);
        Vector2 dir = enemy.transform.position - transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * weaponSO.bulletSpeed;
    }
}
