using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hz_sokutei : MonoBehaviour
{

    public static float Hz = 500;
    public static int GetHz() {
        return ((int)Hz);
    }

    //閾値
    float Hz_high, Hz_low;

    // Start is called before the first frame update
    void Start()
    {
        Hz_high = high_timer.HighVoice();
        Hz_low = low_timer.LowVoice();
        Hz = (Hz_high + Hz_low) / 2;
        Debug.Log(Hz);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}