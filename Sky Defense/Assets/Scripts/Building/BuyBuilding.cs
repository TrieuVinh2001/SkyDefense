using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyBuilding : MonoBehaviour
{
    [SerializeField] private BuildingSO buildSO;
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private TextMeshProUGUI price;

    private void Start()
    {
        price.text = "" + buildSO.priceCrystal;
    }

    public void SelectBuilding()//Tạo nhà
    {
        if (buildSO.priceGem <= GameManager.instance.gem && buildSO.priceCrystal <= GameManager.instance.crystal && buildSO.priceMoney <= GameManager.instance.money
            /*&& buildSO.slotBuilding + GameManager.instance.slotBuild <= GameManager.instance.slotBuildMax*/)
        {
            GameManager.instance.gem -= buildSO.priceGem;
            GameManager.instance.crystal -= buildSO.priceCrystal;
            GameManager.instance.money -= buildSO.priceMoney;
            //GameManager.instance.slotBuild += buildSO.slotBuilding;
            UIManager.instance.UpdateResourcesUI();

            UIManager.instance.buildingBuyPanel.SetActive(false);
            GameObject building = Instantiate(buildingPrefab, UIManager.instance.buildingTranf.parent.position, Quaternion.identity);
            building.transform.parent = UIManager.instance.buildingTranf;

            building.GetComponentInParent<CircleCollider2D>().enabled = false;
            building.transform.parent.GetComponentInParent<SpriteRenderer>().enabled = false;
            building.transform.parent.GetComponentInParent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Debug.Log("not money");
        }
    }
}
