using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    public Queue<KeyValuePair<GameObject, float>> troopQueue = new Queue<KeyValuePair<GameObject, float>>();
    public GameObject loadingBar;
    public List<GameObject> loadingSlots;

    private float timer;
    private float countdown;
    private bool isSet = false;

    private float scaleX;

    private void Start()
    {
        scaleX = loadingBar.transform.localScale.x;
        loadingBar.transform.localScale = new Vector3(scaleX, loadingBar.transform.localScale.y, 0);
    }

    [System.Obsolete]
    private void Update()
    {
        if (troopQueue.Count > 0 && isSet == false)
        {
            timer = troopQueue.Peek().Value;
            countdown = troopQueue.Peek().Value;
            isSet = true;
                    
        }
        if (isSet == true)
        {
            if (countdown < 0)
            {
                troopQueue.Peek().Key.SetActive(true);
                troopQueue.Dequeue();
                isSet = false;
            }
            countdown -= Time.deltaTime;

            float reduce = countdown / timer;
            loadingBar.transform.localScale = new Vector3(scaleX * reduce, loadingBar.transform.localScale.y, 0);
            //bloodBar.GetComponent<SpriteRenderer>().size = new Vector2(bloodBar.GetComponent<SpriteRenderer>().size.x, bloodBar.GetComponent<SpriteRenderer>().size.y - reduce);
        }

        for (int i = 0; i < loadingSlots.Count; i++)
        {
            if (i <= troopQueue.Count - 1)
            {
                loadingSlots[i].SetActive(true);
                continue;
            }
            loadingSlots[i].SetActive(false);
        }
    }

}
