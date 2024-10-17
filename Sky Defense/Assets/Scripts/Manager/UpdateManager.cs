using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private WeaponSO weapon;

    private void OnEnable()
    {
        weapon = UIManager.instance.weaponTranf.GetComponentInChildren<WeaponBase>().weaponSO;
    }

    public void UpdateWeapon()//Nâng cấp súng
    {
        if (weapon.weaponUpdatePrefab == null)
            return;
        if (weapon.priceGemUpdate <= GameManager.instance.gem && weapon.priceCrystalUpdate <= GameManager.instance.crystal && weapon.priceMoneyUpdate <= GameManager.instance.money)
        {
            GameManager.instance.gem -= weapon.priceGemUpdate;
            GameManager.instance.crystal -= weapon.priceCrystalUpdate;
            GameManager.instance.money -= weapon.priceMoneyUpdate;
            UIManager.instance.UpdateResourcesUI();
            
            UIManager.instance.weaponUpdatePanel.SetActive(false);

            GameObject weaponDef = Instantiate(weapon.weaponUpdatePrefab, UIManager.instance.weaponTranf);

            //weaponDef.GetComponentInParent<CircleCollider2D>().enabled = false;
            weaponDef.GetComponentInParent<SpriteRenderer>().enabled = false;
            Destroy(UIManager.instance.weaponTranf.GetChild(0).gameObject);
        }
        else
        {
            UIManager.instance.NoteTextUp();
        }
    }
}
