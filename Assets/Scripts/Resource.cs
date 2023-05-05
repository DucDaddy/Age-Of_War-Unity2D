using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public int gold;
    public int exp;
    public Text goldText;
    public Text expText;

    private void Start()
    {
        this.gold = 1000;
        this.exp = 3000;

        this.goldText = GameObject.Find("Gold").GetComponent<Text>();
        this.expText = GameObject.Find("Exp").GetComponent<Text>();
        goldText.text = gold.ToString();
        expText.text = exp.ToString();
    }
    private void Update()
    {
        goldText.text = gold.ToString();
        expText.text = exp.ToString();
    }

    public void upGold(int gold)
    {
        this.gold += gold;
    }

    public void downGold(int gold)
    {
        this.gold -= gold;
    }

    public void upExp(int exp)
    {
        this.exp += exp;
    }


}
