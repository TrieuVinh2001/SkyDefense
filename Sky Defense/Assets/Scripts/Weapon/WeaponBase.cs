using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponSO weaponSO;

    [SerializeField] protected Transform pointShoot1;
    [SerializeField] protected Transform pointShoot2;
    [SerializeField] protected Transform pointShoot3;

    protected float timeWaitAttack;
    protected GameObject enemy;
    protected bool canAttack = false;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeWaitAttack = weaponSO.timeAttack;
    }

    protected virtual void Update()
    {
        CheckEnemy();
        timeWaitAttack -= Time.deltaTime;

        if (enemy != null)
        {
            Vector2 direction = enemy.transform.position - transform.position;
            transform.right = direction;//Hướng theo enemy

            if (canAttack && timeWaitAttack <= 0)
            {
                timeWaitAttack = weaponSO.timeAttack;
                Shoot();
            }
        }
    }

    protected virtual void CheckEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponSO.attackRange, weaponSO.enemyLayer);
        if (enemies.Length <= 0)
        {
            canAttack = false;
        }
        else
        {
            enemy = enemies[0].gameObject;
            canAttack = true;
        }
    }

    protected virtual void Shoot()
    {
        
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, weaponSO.attackRange);
    }
}
