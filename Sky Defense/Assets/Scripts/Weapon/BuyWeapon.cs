using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private TextMeshProUGUI price;

    private void Start()
    {
        price.text = "" + weaponSO.priceGem;
    }

    public void SelectWeapon()//Tạo súng
    {
        if (weaponSO.priceGem <= GameManager.instance.gem && weaponSO.priceCrystal <= GameManager.instance.crystal && weaponSO.priceMoney <= GameManager.instance.money)
        {
            GameManager.instance.gem -= weaponSO.priceGem;
            GameManager.instance.crystal -= weaponSO.priceCrystal;
            GameManager.instance.money -= weaponSO.priceMoney;
            UIManager.instance.UpdateResourcesUI();

            UIManager.instance.weaponBuyPanel.SetActive(false);
            GameObject weaponDef = Instantiate(weaponPrefab, UIManager.instance.weaponTranf);

            //weaponDef.GetComponentInParent<CircleCollider2D>().enabled = false;
            weaponDef.GetComponentInParent<SpriteRenderer>().enabled = false;
        }
        else
        {
            UIManager.instance.NoteTextUp();
        }
    }
}
