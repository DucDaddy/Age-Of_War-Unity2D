using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    public static List<Transform> troopPositions = new List<Transform>();
    public static List<Transform> botPositions = new List<Transform>();

    //float deltaTime = 0.0f;

    //void Update()
    //{
    //    deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    //}

    //void OnGUI()
    //{
    //    float msec = deltaTime * 1000.0f;
    //    float fps = 1.0f / deltaTime;
    //    float ups = Time.timeScale / deltaTime;
    //    string text = string.Format("FPS: {0:0.}   UPS: {1:0.}   Frame time: {2:0.0} ms", fps, ups, msec);
    //    Debug.Log(text);
    //}

}
