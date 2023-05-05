using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTroop : MonoBehaviour
{
    public GameObject troopPrefab;
    public GameObject allyTroop;
    public GameObject loading;
    public Text infoTroop;

    private void Start()
    {
        allyTroop = GameObject.Find("Ally Troop");
        loading = GameObject.Find("Loading");
    }

    private void OnMouseDown()
    {
        if (loading.GetComponent<LoadingController>().troopQueue.Count < 4 && GameObject.Find("Resources").GetComponent<Resource>().gold >= troopPrefab.GetComponent<TroopController>().cost)
        {
            //StartCoroutine(SpawnNextTroop());
            GameObject newTroop = Instantiate(troopPrefab, new Vector3(-8.86f, troopPrefab.GetComponent<TroopController>().posY, 0), Quaternion.identity, allyTroop.transform);
            newTroop.tag = "Ally";
            newTroop.GetComponent<SpriteRenderer>().sortingOrder = 1;
            newTroop.SetActive(false);
            loading.GetComponent<LoadingController>().troopQueue.Enqueue(new KeyValuePair<GameObject, float>(newTroop, troopPrefab.GetComponent<TroopController>().loadTime));
            GameObject.Find("Resources").GetComponent<Resource>().downGold(troopPrefab.GetComponent<TroopController>().cost);
        }
    }

    private void OnMouseEnter()
    {
        infoTroop.text = troopPrefab.GetComponent<TroopController>().cost.ToString() + "$ - " + troopPrefab.GetComponent<TroopController>().nameTroop;
    }

    private void OnMouseExit()
    {
        infoTroop.text = "";
    }

}
