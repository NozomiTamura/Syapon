using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public GameObject syabon;
    public GameObject atari;
    public GameObject particleObject;

    //時間が過ぎたら当たり判定を消す
    public float countdown = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //見えないSphereが消えたらシャボン玉を消す
        if (atari != true)
        {
            Instantiate(particleObject, this.transform.position, Quaternion.identity);  //パーティクルを生成
            Destroy(syabon.gameObject, 0f);    //オブジェクト消去
        }

        //時間をカウントダウンする
        countdown -= Time.deltaTime;
        if (countdown < 0) {
            Destroy(syabon.gameObject, 0f);
        }
    }
}
