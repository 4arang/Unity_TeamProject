using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Stat_Bar : MonoBehaviour
{
    private const float InGameSocnds = 60f;

    private Text timeText;
    private float time;
    int min;
    int sec;
    StringBuilder timeSB;

    void Start()
    {
        timeSB = new StringBuilder("");
        timeText = transform.Find("Time_Text").GetComponent<Text>();
    }

    void Update()
    {
        time += Time.deltaTime;

        min = Mathf.FloorToInt(time / 60);
        sec = Mathf.FloorToInt(time - min * 60);

        string minString = min.ToString("00");
        string secString = sec.ToString("00");

        timeSB.Clear();

        timeSB.Append(min.ToString("00"));
        timeSB.Append(":");
        timeSB.Append(sec.ToString("00"));

        timeText.text = timeSB.ToString();
    }
}
