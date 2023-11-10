using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TMP_Text timer;
    public float currentTime = 0;
    public float bronzeTime;
    public float silverTime;
    public float goldTime;

    void Update()
    {
        currentTime += Time.deltaTime;
        timer.SetText(string.Format("{0:00}:{1:00}", Mathf.FloorToInt(currentTime / 60), Mathf.FloorToInt(currentTime % 60)));
    }
}