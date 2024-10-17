using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBoundary : MonoBehaviour
{
    private GameObject building;

    private void Start()
    {
        building = transform.GetChild(0).gameObject;
        building.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ShipController>(out ShipController ship))
        {
            building.SetActive(true);
            building.GetComponent<CircleCollider2D>().enabled = true;
            building.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ShipController>(out ShipController ship))
        {
            building.GetComponent<CircleCollider2D>().enabled = false;
            building.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
