using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBase : MonoBehaviour
{
    public BuildingSO buildingSO;
    [SerializeField] protected float healthCurrent;
    [SerializeField] protected Image healthImage;
    [SerializeField] protected GameObject explosionPrefab;

    protected virtual void Start()
    {
        healthCurrent = buildingSO.health;
    }

    protected virtual void Update()
    {
        healthImage.fillAmount = healthCurrent / buildingSO.health;

        //if (hpEffectImage.fillAmount > hpImage.fillAmount)
        //{
        //    hpEffectImage.fillAmount -= hurtSpeed;
        //}
        //else
        //{
        //    hpEffectImage.fillAmount = hpImage.fillAmount;
        //}
    }

    public void TakeDamage(float damage)
    {
        if (healthCurrent - damage > 0)
        {
            healthCurrent -= damage;
        }
        else
        {
            Destroy();
        }
    }

    public void RepairBuildingFull()
    {
        healthCurrent = buildingSO.health;
    }

    protected virtual void Destroy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
