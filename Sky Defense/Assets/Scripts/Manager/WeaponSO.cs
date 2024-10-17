using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName ="Weapon")]
public class WeaponSO : ScriptableObject
{
    public int level;
    public int priceGem;
    public int priceCrystal;
    public int priceMoney;
    public int priceGemUpdate;
    public int priceCrystalUpdate;
    public int priceMoneyUpdate;
    public float timeAttack;
    public float attackRange;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public GameObject weaponUpdatePrefab;
    public LayerMask enemyLayer;
}
