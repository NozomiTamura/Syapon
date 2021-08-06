using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public int minScale = 1;  //最小値
    public int maxScale = 7;  //最大値

    public ParticleSystem main;
    public GameObject atari;
    public GameObject particleObject;

    // Start is called before the first frame update
    void Start()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        ScaleChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ランダムな数を生成する関数
    private int GetRandomScale()
    {
        int x = 0;

        x = UnityEngine.Random.Range(minScale, maxScale);

        //大きさが2,3,6以外の時はもう1度ランダム生成 (3がデフォルトの大きさ)
        while (x==1 || x==4 || x==5 )
        {
            x = UnityEngine.Random.Range(minScale, maxScale);
        }

        //Debug.Log(x);

        return x;
    }

    //scaleを変更する
    public void ScaleChange() {
        int a = GetRandomScale();
        main.startSize = a;

        float scale;
        //当たり判定の大きさも変更
        if (a == 2)
        {
            scale = 0.3f;
            atari.transform.localScale = new Vector3(scale, scale, scale);
        }
        else if (a == 6)
        {
            scale = 0.9f;
            atari.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
