using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_4 : WeaponBase
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
                Bullet();
                break;
            case 2:
                Bullet();
                break;
            case 3:
                Bullet();
                break;
        }
    }

    private void Bullet()
    {
        GameObject bullet = Instantiate(weaponSO.bulletPrefab, pointShoot1.position, Quaternion.identity);
        Vector2 dir = enemy.transform.position - transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Grape>().enemy = enemy.transform;
    }
}
