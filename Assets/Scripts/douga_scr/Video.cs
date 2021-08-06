using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 
using UnityEngine.SceneManagement;


public class Video : MonoBehaviour
{

    public VideoPlayer videoPlayer;  //アタッチした VideoPlayer をインスペクタでセットする
    float delta = 0;	//数を数えるのに使う
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;    //数を数える
        if ((videoPlayer.isPlaying == false)&(this.delta>2))
        {
            //※ここに終了したときの処理など
            Debug.Log("スタート画面移動");
            SceneManager.LoadScene("2d_start");
            return;
        }
    
    }
}
