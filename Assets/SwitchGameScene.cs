using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchGameScene : MonoBehaviour
{
    public void switchGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
