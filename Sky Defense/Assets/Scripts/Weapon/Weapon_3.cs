using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_3 : WeaponBase
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
                Bullet(2);
                break;
            case 2:
                Bullet(4);
                break;
            case 3:
                Bullet(6);
                break;
        }
    }

    private void Bullet(int damage)
    {
        GameObject bullet = Instantiate(weaponSO.bulletPrefab, pointShoot1.position, Quaternion.identity);
        Vector2 dir = enemy.transform.position - transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Laser>().UpdateLaserRange(damage);
    }
}
