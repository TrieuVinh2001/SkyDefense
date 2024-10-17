using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float healthMax;
    protected float currentHealth;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected Transform pointShoot;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float timeAttack;
    protected float timeWaitAttack;
    [SerializeField] protected LayerMask layerAttack;
    [SerializeField] protected GameObject explosionPrefab;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        currentHealth = healthMax;
        rb = GetComponent<Rigidbody2D>();

        if (!ObjectPooling.instance.bulletPools.ContainsKey(bulletPrefab.name))
        {
            ObjectPooling.instance.AddBulletType(bulletPrefab.name, bulletPrefab, 20);
        }
    }

    protected virtual void Update()
    {
        
    }

    public virtual void TakeDame(float damage)
    {
        if (currentHealth - damage > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            Destroy();
        }
    }

    protected virtual void Destroy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
