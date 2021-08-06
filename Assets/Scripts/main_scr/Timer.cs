using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //カウントダウン
    public float countdown = 33.0f;
    //タイムアップ用のパネル
    public GameObject panel;
    public Text panelText;
    float cd = 3.0f;

    //時間を表示するText型の変数
    public Text timerText;

    int flag = 0;
    int count = 0;

    //スコアによってリザルト画面を変える
    //public Text scoreText;
    float score;

    //BGM
    public AudioClip sound;
    public AudioClip sound_FEVER;
    AudioSource audioSource;

    //最初のカウントダウン音
    public AudioClip pon;
    public AudioClip pooon;

    //SP割れるときの情報欲しいため
    Player_Script script;
    GameObject SP_obj;

    // Start is called before the first frame update
    void Start()
    {
        //自分のインスペクター内からTextコンポーネントを取得
        timerText = GetComponent<Text>();
        Time.timeScale = 1f;
        cd = 3.0f;
        //
        panel.SetActive(true);
        Time.timeScale = 0f;
        //panelText.text = "3";

        SP_obj = GameObject.Find("Player");
        script = SP_obj.GetComponent<Player_Script>();

        //音
        audioSource = GetComponent<AudioSource>();

        flag = 0;
        count=0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 0)
        {
            //cd -= 0.025f;
            cd -= 0.008f;

            if (cd <= 2.9 && cd > 2) {
                panelText.text = "3";
                if (count == 0)
                {
                    audioSource.PlayOneShot(pon);  //音を再生
                    count++;
                }
            }
            else if (cd <= 2 && cd > 1)
            {
                panelText.text = "2";
                if (count == 1)
                {
                    audioSource.PlayOneShot(pon);  //音を再生
                    count++;
                }
            }
            else if (cd <= 1 && cd > 0)
            {
                panelText.text = "1";
                if (count == 2)
                {
                    audioSource.PlayOneShot(pon);  //音を再生
                    count++;
                }
            }
            else if (cd <= 0)
            {
                panel.SetActive(false);
                if (count == 3)
                {
                    audioSource.PlayOneShot(pooon);  //音を再生
                    count++;
                }
                Time.timeScale = 1f;
                flag = 1;
            }

        }
        else
        {
            //時間をカウントダウンする
            countdown -= Time.deltaTime;
            byo();

            int SP_flag = script.GetSPfrag();

            if (flag == 1)
            {
                audioSource.PlayOneShot(sound);  //音を再生
                flag++;
            }else if(flag == 2 && SP_flag == 1){
                audioSource.Stop();
                audioSource.PlayOneShot(sound_FEVER);  //音を再生
                flag++;

            }

            //時間を表示する
            //timerText.text = countdown.ToString("f0") + "秒";
            timerText.text = countdown.ToString("f1") + "秒";


            //countdownが０以下になった時
            if (countdown <= 0)
            {
                timerText.text = "0.0秒";
                //timerText.text = "時間になりました!";
                countdown -= 0.01f;
                Time.timeScale = 0f;
                panel.SetActive(true);
                panelText.text = "TIME UP";
                //Debug.Log(countdown);
                //3秒後にCall関数を実行する
                if (countdown <= -3)
                {
                    Call();
                }
            }
        }

        void Call()
        {
            Debug.Log("シーン移行");

            //スコアを取得
            score = ScoreManager.GetScore();
            if (5000 <= score)
            {
                SceneManager.LoadScene("2d_result_1ohiru");
            }
            else if (1000 <= score && score < 5000)
            {
                SceneManager.LoadScene("2d_result_2kumori");
            }
            else if (score < 1000)
            {
                SceneManager.LoadScene("2d_result_3yoru");
            }
            //SceneManager.LoadScene("2d_result");
        }
    }
    public float byo() {
        return countdown;
    }
}
