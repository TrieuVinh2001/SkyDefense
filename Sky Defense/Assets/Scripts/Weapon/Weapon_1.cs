using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_1 : WeaponBase
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
        switch (weaponSO.level)
        {
            case 1:
                Bullet(pointShoot1);
                break;
            case 2:
                Bullet(pointShoot1);
                Bullet(pointShoot2);
                break;
            case 3:
                Bullet(pointShoot1);
                Bullet(pointShoot2);
                Bullet(pointShoot3);
                break;

        }
    }

    private void Bullet(Transform tranf)
    {
        GameObject bullet = Instantiate(weaponSO.bulletPrefab, tranf.position, Quaternion.identity);
        Vector2 dir = enemy.transform.position - transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * 3;
    }
}
