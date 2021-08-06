using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvControl : MonoBehaviour
{
    ScoreControl score_script;
    public GameObject scoretx;
    //GameObject scoretx;

    float hei;
    float hei_down;

    double w = 10.00;
    double h = 1.00;

    public GameObject eki;
    int flag = 0;

    // Start is called before the first frame update
    void Start()
    {
        //scoretx = GameObject.Find("Score");
        score_script = scoretx.GetComponent<ScoreControl>();


        hei = score_script.GetScore();
        hei_down = score_script.GetScore();
        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == 0){
            if (hei_down > 0)
            {
                //Height 最小1-最大20
                //最大スコア2000の場合
                //Height0.1=10
                h += 0.01;
                eki.GetComponent<RectTransform>().sizeDelta = new Vector2((float) w, (float)h);

                hei_down -= 1;


            }else{
                flag =1;
            }
        }

    }
    public int GetFlag(){
        return flag;
    }
}
