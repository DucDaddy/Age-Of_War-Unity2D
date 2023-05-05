using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLoading : MonoBehaviour
{
    public Queue<KeyValuePair<GameObject, float>> troopQueue = new Queue<KeyValuePair<GameObject, float>>();

    private float countdown;
    private bool isSet = false;

    [System.Obsolete]
    private void Update()
    {
        if (troopQueue.Count > 0 && isSet == false)
        {
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
        }
    }
}
