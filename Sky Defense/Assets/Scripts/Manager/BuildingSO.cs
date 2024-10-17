using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingSO : ScriptableObject
{
    public float health;
    public int priceGem;
    public int priceCrystal;
    public int priceMoney;
    public int priceGemUpdate;
    public int priceCrystalUpdate;
    public int priceMoneyUpdate;
    public int slotBuilding;
    public GameObject explosion;
}
