using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //スコア
    static float score = 0.0f;

    //スコアを表示するText型の変数
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "Score: " + score.ToString("f0") + "点";
    }

    // Update is called once per frame
    void Update()
    {
        //スコアを表示する
        scoreText.text = "Score: " + score.ToString("f0") + "点";
    }

    //スコアを増加する
    public void AddScore(int amount)
    {   //外部からアクセスするのでpublic
        score += amount;
        scoreText.text = "Score: " + score.ToString("f0") + "点";
    }
    public static float GetScore()
    {
        return score;
    }
}
