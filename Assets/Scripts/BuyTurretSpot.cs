using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTurretSpot : MonoBehaviour
{
    public int cost;
    public GameObject turretSpots;

    private void Start()
    {
        cost = 1000;
    }

    [System.Obsolete]
    private void OnMouseDown()
    {
        foreach (Transform spot in turretSpots.transform)
        {
            if (spot.gameObject.activeSelf == false && this.cost <= GameObject.Find("Resources").GetComponent<Resource>().gold)
            {
                spot.gameObject.SetActive(true);
                GameObject.Find("Resources").GetComponent<Resource>().downGold(this.cost);
                return;
            }
        }
    }

}
