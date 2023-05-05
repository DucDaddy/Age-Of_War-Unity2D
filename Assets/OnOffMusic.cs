using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMusic : MonoBehaviour
{
    public GameObject onMusic;
    public GameObject offMusic;

    private bool isPlayMusic;

    private void Start()
    {
        isPlayMusic = true;
        PlayerPrefs.SetInt("IsPlayMusic", 0);
    }

    private void OnMouseDown()
    {
        switchOrderLayout();
        isPlayMusic = !isPlayMusic;
        PlayerPrefs.SetInt("IsPlayMusic", isPlayMusic ? 0 : 1);
    }

    private void switchOrderLayout()
    {
        int ol1, ol2;
        ol1 = onMusic.GetComponent<SpriteRenderer>().sortingOrder;
        ol2 = offMusic.GetComponent<SpriteRenderer>().sortingOrder;

        onMusic.GetComponent<SpriteRenderer>().sortingOrder = ol2;
        offMusic.GetComponent<SpriteRenderer>().sortingOrder = ol1;
    }
}
