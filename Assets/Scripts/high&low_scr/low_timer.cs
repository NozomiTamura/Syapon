using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class low_timer : MonoBehaviour
{
    //カウントダウン
    public float countdown;
    float count = 3.0f;

    //時間を表示するText型の変数
    public Text timerText;

    //文章の表示、非表示
    public GameObject bunsyo;
    public GameObject count3;
    public Text bunsyoText;
    public Text count3Text;

    //周波数の更新
    private FreqManager fm;
    GameObject freq;

    private HighControl HighControl;
    GameObject hc;
    public int f;

    public static int lowVoice=0;

    int flag = 0;

    // Start is called before the first frame update
    void Start()
    {
        //自分のインスペクター内からTextコンポーネントを取得
        timerText = GetComponent<Text>();

        //周波数関係
        freq = GameObject.Find("FreqManager");
        fm = freq.GetComponent<FreqManager>();

        hc = GameObject.Find("HighControl");
        HighControl = hc.GetComponent<HighControl>();

        lowVoice = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;
        byo();

        f = HighControl.High();

        //時間を表示する　確かめる用
        timerText.text = countdown.ToString("f1") + "秒";

        //fm.Freq(f);

        if (countdown < 25)
        {
            bunsyo.SetActive(false);
            count3.SetActive(true);
        }
        if (countdown < 24)
        {
            count3Text.text = "2";
        }
        if (countdown < 23)
        {
            count3Text.text = "1";
        }
        if (countdown < 22) {
            count3.SetActive(false);
            bunsyoText.text = "低い声を出してください";
            bunsyo.SetActive(true);
            flag = 1;
        }

        if (flag == 1) {
            //声出してもらう
            fm.Freq(f);
        }

        if (countdown < 19)
        {
            flag = 0;
            lowVoice = f;
            Debug.Log(lowVoice);
            Debug.Log("シーン移行");
            SceneManager.LoadScene("2d_start");
        }

    }

    void Call()
    {
        Debug.Log("シーン移行");
        SceneManager.LoadScene("2d_start");
    }

    public float byo() {
        return countdown;
    }

    public static int LowVoice()
    {
        return lowVoice;
    }
}
