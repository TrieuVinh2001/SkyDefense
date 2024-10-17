using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerClick;
    [SerializeField] private LayerMask build;
    [SerializeField] private LayerMask weaponDefense;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())//Nếu nhấn vào UI
            {
                return;
            }

            Click();
        }
    }

    private void Click()
    {
        Vector2 mousePos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero, 0, layerClick);//xác định điểm nhấn dựa vào raycast
        RaycastHit2D hitBuild = Physics2D.Raycast(mousePos2D, Vector2.zero, 0, build);
        RaycastHit2D hitWeapon = Physics2D.Raycast(mousePos2D, Vector2.zero, 0, weaponDefense);
        if (hits.Length > 0)
        {
            RaycastHit2D topHit = hits[0];
            
            foreach (RaycastHit2D hit in hits)
            {
                SpriteRenderer renderer = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                if (renderer != null && renderer.sortingOrder > topHit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder)
                {
                    topHit = hit;
                }
            }

            if (topHit.collider.name == hitBuild.collider.name)
            {
                ClickSlotBuilding(topHit.collider);
            }
            else if (topHit.collider.name == hitWeapon.collider.name)
            {
                ClickSlotWeapon(topHit.collider);
            }
        }
    }

    private void ClickSlotBuilding(Collider2D topHit)
    {
        if (topHit.TryGetComponent<BuildingBase>(out BuildingBase build))
        {
            UIManager.instance.DestoyBuildingPanel(topHit.transform);
        }
        else
        {
            UIManager.instance.BuyBuilding(topHit.transform);
        }
    }

    private void ClickSlotWeapon(Collider2D topHit)
    {
        if (topHit.transform.childCount == 0)
        {
            UIManager.instance.BuyWeapon(topHit.transform);
        }
        else
        {
            UIManager.instance.UpdateWeapon(topHit.transform);
        }
    }
}
