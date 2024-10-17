using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform pointShoot;



    private int facingDir = 1;
    private bool facingRight = true;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * moveSpeed;

        if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPooling.instance.GetBullet("FireBullet");
        bullet.GetComponent<Transform>().position = pointShoot.position;
        bullet.SetActive(true);
        bullet.GetComponent<Transform>().localScale = new Vector3(facingDir, 1, 1);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed * facingDir;
        StartCoroutine(DeactivateBullet(bullet));
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        //transform.localScale = new Vector3(1 * facingDir, 1, 1);
    }

    private IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2);
        bullet.SetActive(false);
    }
}
