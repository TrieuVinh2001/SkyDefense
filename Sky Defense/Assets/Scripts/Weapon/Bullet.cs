using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    public LayerMask layerAttack;

    private void OnEnable()
    {
        StartCoroutine(DeactivateBullet());
    }

    private void Update()
    {
        Collider2D[] layerCheck = Physics2D.OverlapCircleAll(transform.position, 0.01f, layerAttack);
        if (layerCheck.Length > 0)
        {
            if(layerCheck[0].TryGetComponent<ShipBase>(out ShipBase ship))
            {
                ship.TakeDame(damage);
                ResetBullet();
            }
            else if (layerCheck[0].TryGetComponent<BuildingBase>(out BuildingBase build))
            {
                build.TakeDamage(damage);
                ResetBullet();
            }
        }
    }

    private IEnumerator DeactivateBullet()
    {
        yield return new WaitForSeconds(3.5f);
        ResetBullet();
    }

    private void ResetBullet()
    {
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }
}
