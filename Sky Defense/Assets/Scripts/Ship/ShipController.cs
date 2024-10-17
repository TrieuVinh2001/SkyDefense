using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : ShipBase
{
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] protected Image healthImage;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        healthImage.fillAmount = currentHealth / healthMax;

        // movement
        int spin = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            spin += 1;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            spin -= 1;
        }

        ShipRotate(spin);

        int moveForce = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.J))
        {
            moveForce += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.K))
        {
            moveForce -= 1;
        }

        Movement(moveForce);

        timeWaitAttack -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timeWaitAttack <= 0)
        {
            timeWaitAttack = timeAttack;
            Shoot();
        }
    }

    private void Movement(int moveForce)
    {
        Vector3 thrustVec = transform.right * (moveForce * moveSpeed);
        //rb.AddForce(thrustVec);
        rb.velocity = thrustVec;
    }

    private void ShipRotate(int spin)
    {
        float rotate = spin * rotateSpeed;
        rb.angularVelocity = rotate;
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPooling.instance.GetBullet(bulletPrefab.name);
        bullet.transform.position = pointShoot.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Bullet>().layerAttack = layerAttack;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * (pointShoot.position - transform.position).normalized ;
    }

    protected override void Destroy()
    {
        base.Destroy();
        StartCoroutine(ReSpawnShip());
    }

    private IEnumerator ReSpawnShip()
    {
        transform.position = Vector3.zero;
        currentHealth = healthMax;
        //gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        //gameObject.SetActive(true);
    }
}
