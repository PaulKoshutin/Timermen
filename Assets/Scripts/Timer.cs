using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static int Hour;
    public static int Minute;
    public static int Second;

    public int hour;
    public int minute;
    public int second;

    void Awake()
    {
        Hour = 0;
        Minute= 0;
        Second = 0;
    }
    void FixedUpdate()
    {
        TimeCalculation();
        hour = Hour;
        minute = Minute;
        second = Second;

        TMPro.TextMeshProUGUI mText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        mText.text = hour + ":" + minute + ":" + second;
    }
    void TimeCalculation()
    {
        Second += 1;
        if (Second > 60)
        {
            Second = 0;
            Minute++;
        }
        if (Minute > 60)
        {
            Minute = 0;
            Hour++;
        }
        if (Hour > 23)
            Hour = 0;
    }
}
