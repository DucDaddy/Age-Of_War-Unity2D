using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject currentStrongHold;
    public GameObject nextStrongHold;
    public GameObject turretSpot;
    public GameObject music;
    public Sprite sprite1;
    public Sprite sprite2;

    private bool isPlay;

    private void Start()
    {
        isPlay = true;
        if (PlayerPrefs.GetInt("IsPlayMusic", 0) == 1)
        {
            music.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlay = !isPlay;
        }
        if (isPlay == true)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void NextAge()
    {
        currentStrongHold.SetActive(false);
        nextStrongHold.SetActive(true);

        turretSpot.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite1;
        turretSpot.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = sprite2;
        turretSpot.transform.GetChild(1).transform.localPosition = new Vector3(-7.04f, 0.95f, 0);
        turretSpot.transform.GetChild(2).transform.localPosition = new Vector3(-7.04f, 2.38f, 0);
    }
}
