using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreqManager : MonoBehaviour
{
    //周波数
    static float score = 0.0f;

    //周波数を表示するText型の変数
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        scoreText = GameObject.Find("Freq").GetComponent<Text>();
        scoreText.text = "Freq: " + score.ToString("f0");
    }

    // Update is called once per frame
    void Update()
    {
        //周波数を表示する
        scoreText.text = "Freq: " + score.ToString("f0");
    }

    //周波数を変更する
    public void Freq(int amount)
    {   //外部からアクセスするのでpublic
        score = amount;
        scoreText.text = "Freq: " + score.ToString("f0");
    }
    public static float GetScore()
    {
        return score;
    }
}
