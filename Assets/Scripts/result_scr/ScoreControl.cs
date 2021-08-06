using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    // Start is called before the first frame update

    Text scoreText;
    float score;


    void Start()
    {
        //float score = maingame.GetScore();
        score = ScoreManager.GetScore();

        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "Score: " + score + "点";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetScore()
    {
        Debug.Log(score);
        return score;
    }
}
