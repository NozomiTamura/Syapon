using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_RB : MonoBehaviour
{
    //プレハブ
    public GameObject prefab_Blue;
    public GameObject prefab_Red;

    public float minTime = 0f;  //時間間隔の最小値
    public float maxTime = 2f;  //時間間隔の最大値
    private float interval;     //生成時間間隔
    private float time = 0f;    //経過時間

    //座標系
    [Header("Set Y Position Min and Max")]
    public float xMinPosition = -13f;   //X座標の最小値
    public float xMaxPosition = 13f;    //X座標の最大値
    public float yMinPosition = -20f;     //Y座標の最小値
    public float yMaxPosition = 20f;    //Y座標の最大値

    //秒数
    private float count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;
        count += Time.deltaTime;

        //Debug.Log(count);

        if (count > 0)
        {
            //経過時間が生成時間になった時（生成時間より大きくなった時）
            if (time > interval)
            {
                Blue();
                Red();
            }
        }
    }

    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(minTime, maxTime);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        float x = 0, y = 0, z = 0;

        while (Mathf.Abs(x) < 4 && Mathf.Abs(y) < 4)
        {
            //それぞれの座標をランダムに生成する
            x = UnityEngine.Random.Range(xMinPosition, xMaxPosition);
            y = UnityEngine.Random.Range(yMinPosition, yMaxPosition);
            z = 0;
        }

        return new Vector3(x, y, z);
    }

    //青生成
    private void Blue()
    {
        //プレハブをインスタンス化する（生成する）
        GameObject Blue = Instantiate(prefab_Blue);
        //ランダムで座標位置を設定
        Blue.transform.position = GetRandomPosition();
        //経過時間を初期化して再度時間計測を始める
        time = 0f;
        //次に発生する時間間隔を決定する
        interval = 0.3f;
    }

    //赤生成
    private void Red()
    {
        //プレハブをインスタンス化する（生成する）
        GameObject Red = Instantiate(prefab_Red);
        //ランダムで座標位置を設定
        Red.transform.position = GetRandomPosition();
        //経過時間を初期化して再度時間計測を始める
        time = 0f;
        //次に発生する時間間隔を決定する
        interval = 0.3f;
    }
}
