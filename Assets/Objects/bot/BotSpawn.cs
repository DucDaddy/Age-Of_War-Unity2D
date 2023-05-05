using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawn : MonoBehaviour
{
    public float timeNextAge;
    public List<GameObject> troop1Prefabs;
    public List<GameObject> troop2Prefabs;

    public GameObject enemyTroop;
    public GameObject loading;

    private float countDown;

    private void Start()
    {
        enemyTroop = GameObject.Find("Enemy Troop");
        loading = GameObject.Find("Bot");
    }

    private void Update()
    {
        if (enemyTroop.transform.childCount < 4)
        {
            int index = Random.Range(0, 3);
            GameObject troopPrefab = troop1Prefabs[index];
            if (loading.GetComponent<BotLoading>().troopQueue.Count < 4)
            {
                GameObject newTroop = Instantiate(troopPrefab, new Vector3(8.86f, troopPrefab.GetComponent<TroopController>().posY, 0), Quaternion.identity, enemyTroop.transform);
                newTroop.tag = "Enemy";
                newTroop.GetComponent<SpriteRenderer>().sortingOrder = 1;
                newTroop.GetComponent<SpriteRenderer>().flipX = true;
                newTroop.SetActive(false);
                loading.GetComponent<BotLoading>().troopQueue.Enqueue(new KeyValuePair<GameObject, float>(newTroop, troopPrefab.GetComponent<TroopController>().loadTime));
            }
        }
    }
}
