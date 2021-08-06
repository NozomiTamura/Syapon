using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazike_dele : MonoBehaviour
{
    //カウントダウン
    public float countdown = 0.5f;

    //消すやつ格納
    public GameObject hazike;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;

        //countdownが０以下になった時
        if (countdown <= 0)
        {
            Destroy(hazike.gameObject, 0f);    //消去
        }
    }
}
