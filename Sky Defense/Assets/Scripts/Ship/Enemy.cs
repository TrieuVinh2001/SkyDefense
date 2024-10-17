using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ShipBase
{
    private float currentSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private int bulletPerBurst;
    [SerializeField] private float timeBetweenBPB;

    [SerializeField] private int moneyBonus;

    private bool canAttack;
    private GameObject target;

    protected override void Start()
    {
        base.Start();
        currentSpeed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();

        CheckPlayer();

        rb.velocity = Vector2.right * currentSpeed * transform.localScale.x;

        timeWaitAttack -= Time.deltaTime;

        if (canAttack && timeWaitAttack <= 0)
        {
            timeWaitAttack = timeAttack;
            StartCoroutine(ShootBullet());
        }
    }
    private void CheckPlayer()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange, layerAttack);
        if (targets.Length <= 0)
        {
            canAttack = false;
            currentSpeed = moveSpeed;

            if (transform.position.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            //if (!targets[0].TryGetComponent<BuildingBase>(out BuildingBase build0) && targets.Length > 1)
            //{
            //    target = targets[1].gameObject;
            //}
            //else/* if (tagets[0].TryGetComponent<BuildingBase>(out BuildingBase build) || tagets[0].TryGetComponent<ShipBase>(out ShipBase ship))*/
            //{
            //    target = targets[0].gameObject;
            //}

            foreach (var targetShoot in targets)
            {
                if(targetShoot.CompareTag("Build"))
                {
                    //Debug.Log(targetShoot.name);
                }
                else
                {
                    target = targetShoot.gameObject;
                }
            }

            if (target != null)
            {
                currentSpeed = 0;
                canAttack = true;
                if (transform.position.x < target.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }

    private IEnumerator ShootBullet()
    {
        for (int i = 0; i < bulletPerBurst; i++)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenBPB);
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPooling.instance.GetBullet(bulletPrefab.name);
        if (bullet != null && target != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = pointShoot.position;
            bullet.GetComponent<Bullet>().layerAttack = layerAttack;
            Vector2 dir = target.transform.position - pointShoot.transform.position;
            float swipeAngle = Mathf.Atan2(dir.y, dir.x);
            swipeAngle = swipeAngle * 180 / Mathf.PI;//Tính góc bắn dựa để chỉnh góc quay đạn
            bullet.transform.Rotate(new Vector3(0, 0, swipeAngle));
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;
        }
        if (bullet == null)
        {
            // Nếu không có đạn sẵn có trong pool, có thể mở rộng pool
            ObjectPooling.instance.ExpandPool(bulletPrefab.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<ShipController>(out ShipController ship))
        {
            ship.TakeDame(3);
        }
    }

    protected override void Destroy()
    {
        base.Destroy();
        GameManager.instance.Money(moneyBonus);
        GameManager.instance.ExplosionSFX();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
