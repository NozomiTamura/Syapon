using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class High_Player_Script : MonoBehaviour
{
    public AudioSource sound_break;
    public AudioSource sound_hp;
    public AudioSource sound_sp;

    public GameObject wave_B;
    public GameObject wave_R;
    public GameObject wave_G;

    //
    BallControl script; //UnityChanScriptが入る変数
    GameObject indext;

    //接触時間の制限　瞬間触れただけで壊れるから
    public float countdown = 0.5f;

    //スコア関連の追加
    public int scoreValue;  //これが得られる点数
    private ScoreManager sm;
    GameObject score;

    //プレイヤーの色　＝　声の高さ
    private GameObject player;

    //マテリアル
    public Material red;
    public Material green;
    public Material blue;

    //hazikeの大きさを決めるのに必要なので
    public GameObject particleObject;

    //壊れた個数を数える
    public static float red_count, red_count_small, red_count_big;
    public static float green_count, green_count_small, green_count_big;
    public static float blue_count, blue_count_small, blue_count_big;

    //HP実装
    int syabon_hp = 30;

    //フラグの設定
    int flag;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSourceコンポーネントを取得し、変数に格納
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound_break = audioSources[0];
        sound_hp = audioSources[1];
        sound_sp = audioSources[2];

        indext = GameObject.Find("GameObject");
        script = indext.GetComponent<BallControl>();

        //playerを取得
        player = GameObject.Find("Player");

        //個数リセット
        red_count = red_count_small = red_count_big = 0;
        green_count = green_count_small = green_count_big = 0;
        blue_count = blue_count_small = blue_count_big = 0;

        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //接触しているオブジェクトを破壊
    void OnTriggerStay(Collider obj) //OnCollisionStay(Collision obj)
    {
        //Debug.Log("接触中");
        int maxIndex = script.GetMaxIndext();

        int freq = script.Freq();
        //Debug.Log(freq);

        //シャボンの大きさを判定
        float scale;
        float syabonScale = obj.transform.localScale.x;

        //hazikeの範囲も変更（泡の大きさは変わってないよ）
        if (syabonScale == 0.3f)
        {
            scale = 0.4f;
            particleObject.transform.localScale = new Vector3(scale, scale, scale);
            scoreValue = 100;
        }
        else if (syabonScale == 0.5f)
        {
            scale = 1f;
            particleObject.transform.localScale = new Vector3(scale, scale, scale);
            scoreValue = 50;
        }
        else if (syabonScale == 0.9f)
        {
            scale = 1.3f;
            particleObject.transform.localScale = new Vector3(scale, scale, scale);
            scoreValue = 10;
        }

        if (flag == 0)
        {
            //赤
            if (obj.gameObject.tag == "Red")
            {
                //時間をカウントダウンする
                countdown -= Time.deltaTime;

                if (freq == 0 && countdown <= 0)
                {
                    Destroy(obj.gameObject, 0f);    //オブジェクト消去
                    sound_break.PlayOneShot(sound_break.clip);  //音を再生
                                                              //Debug.Log("Red Delete");

                    //エフェクト
                    float wavesize = script.GetvolumeRate();
                    GameObject Wave = Instantiate(wave_R);
                    //Wave.transform.localScale = new Vector3(1 * wavesize, 1 * wavesize, 1 * wavesize);
                    Wave.transform.localScale = new Vector3(1 * obj.transform.localScale.x, 1 * obj.transform.localScale.y, 1 * obj.transform.localScale.z);
                    Wave.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                    countdown = 0.5f;   //時間リセット

                    if (scoreValue == 100)
                    {
                        red_count_small += 1;
                    }
                    else if (scoreValue == 50)
                    {
                        red_count += 1;
                    }
                    else if (scoreValue == 10)
                    {
                        red_count_big += 1;
                    }

                    sm.AddScore(scoreValue);    //得点
                }
            }
            //緑
            if (obj.gameObject.tag == "Green")
            {
                //時間をカウントダウンする
                countdown -= Time.deltaTime;

                //Debug.Log(countdown);

                if (freq == 1 && countdown <= 0)
                {
                    Destroy(obj.gameObject, 0f);
                    sound_break.PlayOneShot(sound_break.clip);
                    Debug.Log("Green Delete");

                    //エフェクト
                    float wavesize = script.GetvolumeRate();
                    GameObject Wave = Instantiate(wave_G);
                    //Wave.transform.localScale = new Vector3(1 * wavesize, 1 * wavesize, 1 * wavesize);
                    Wave.transform.localScale = new Vector3(1 * obj.transform.localScale.x, 1 * obj.transform.localScale.y, 1 * obj.transform.localScale.z);
                    Wave.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                    countdown = 0.5f;   //時間リセット

                    if (scoreValue == 100)
                    {
                        green_count_small += 1;
                    }
                    else if (scoreValue == 50)
                    {
                        green_count += 1;
                    }
                    else if (scoreValue == 10)
                    {
                        green_count_big += 1;
                    }

                    sm.AddScore(scoreValue);    //得点
                }
            }
            //青
            if (obj.gameObject.tag == "Blue")
            {
                //時間をカウントダウンする
                countdown -= Time.deltaTime;

                if (freq == 2 && countdown <= 0)
                {
                    Destroy(obj.gameObject, 0f);
                    sound_break.PlayOneShot(sound_break.clip);
                    Debug.Log("Blue Delete");

                    //エフェクト
                    float wavesize = script.GetvolumeRate();
                    GameObject Wave = Instantiate(wave_B);
                    //Wave.transform.localScale = new Vector3(1 * wavesize, 1 * wavesize, 1 * wavesize);
                    Wave.transform.localScale = new Vector3(1 * obj.transform.localScale.x, 1 * obj.transform.localScale.y, 1 * obj.transform.localScale.z);
                    Wave.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                    countdown = 0.5f;   //時間リセット

                    if (scoreValue == 100)
                    {
                        blue_count_small += 1;
                    }
                    else if (scoreValue == 50)
                    {
                        blue_count += 1;
                    }
                    else if (scoreValue == 10)
                    {
                        blue_count_big += 1;
                    }

                    sm.AddScore(scoreValue);    //得点
                }
            }

            //SP
            if (obj.gameObject.tag == "SP")
            {
                //Debug.Log(syabon_hp);
                syabon_hp--;
                if (syabon_hp % 3 == 0)
                {
                    sound_hp.PlayOneShot(sound_sp.clip);
                }

                //どんな声でも壊れる
                if (freq == 0 && syabon_hp <= 0 || freq == 1 && syabon_hp <= 0 || freq == 2 && syabon_hp <= 0)
                //if (freq == 0 || freq == 1 || freq == 2)
                {
                    Destroy(obj.gameObject, 0f);
                    sound_break.PlayOneShot(sound_break.clip);

                    //エフェクト
                    float wavesize = script.GetvolumeRate();
                    GameObject Wave = Instantiate(wave_G);
                    //Wave.transform.localScale = new Vector3(1 * wavesize, 1 * wavesize, 1 * wavesize);
                    Wave.transform.localScale = new Vector3(1 * obj.transform.localScale.x, 1 * obj.transform.localScale.y, 1 * obj.transform.localScale.z);
                    Wave.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                    countdown = 0.5f;   //時間リセット

                    if (scoreValue == 100)
                    {
                        blue_count_small += 1;
                    }
                    else if (scoreValue == 50)
                    {
                        blue_count += 1;
                    }
                    else if (scoreValue == 10)
                    {
                        blue_count_big += 1;
                    }

                    sm.AddScore(scoreValue);    //得点

                    flag = 1;   //壊したい放題へ
                }
            }
        }
        else if (flag == 1) {
            //赤
            if (obj.gameObject.tag == "Red")
            {
                //時間をカウントダウンする
                countdown -= Time.deltaTime;

                if (countdown <= 0)
                {
                    Destroy(obj.gameObject, 0f);    //オブジェクト消去
                    sound_break.PlayOneShot(sound_break.clip);  //音を再生
                                                              //Debug.Log("Red Delete");

                    //エフェクト
                    float wavesize = script.GetvolumeRate();
                    GameObject Wave = Instantiate(wave_R);
                    //Wave.transform.localScale = new Vector3(1 * wavesize, 1 * wavesize, 1 * wavesize);
                    Wave.transform.localScale = new Vector3(1 * obj.transform.localScale.x, 1 * obj.transform.localScale.y, 1 * obj.transform.localScale.z);
                    Wave.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                    countdown = 0.5f;   //時間リセット

                    if (scoreValue == 100)
                    {
                        red_count_small += 1;
                    }
                    else if (scoreValue == 50)
                    {
                        red_count += 1;
                    }
                    else if (scoreValue == 10)
                    {
                        red_count_big += 1;
                    }

                    sm.AddScore(scoreValue);    //得点
                }
            }
            //緑
            if (obj.gameObject.tag == "Green")
            {
                //時間をカウントダウンする
                countdown -= Time.deltaTime;

                //Debug.Log(countdown);

                if (countdown <= 0)
                {
                    Destroy(obj.gameObject, 0f);
                    sound_break.PlayOneShot(sound_break.clip);
                    Debug.Log("Green Delete");

                    //エフェクト
                    float wavesize = script.GetvolumeRate();
                    GameObject Wave = Instantiate(wave_G);
                    //Wave.transform.localScale = new Vector3(1 * wavesize, 1 * wavesize, 1 * wavesize);
                    Wave.transform.localScale = new Vector3(1 * obj.transform.localScale.x, 1 * obj.transform.localScale.y, 1 * obj.transform.localScale.z);
                    Wave.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                    countdown = 0.5f;   //時間リセット

                    if (scoreValue == 100)
                    {
                        green_count_small += 1;
                    }
                    else if (scoreValue == 50)
                    {
                        green_count += 1;
                    }
                    else if (scoreValue == 10)
                    {
                        green_count_big += 1;
                    }

                    sm.AddScore(scoreValue);    //得点
                }
            }
            //青
            if (obj.gameObject.tag == "Blue")
            {
                //時間をカウントダウンする
                countdown -= Time.deltaTime;

                if (countdown <= 0)
                {
                    Destroy(obj.gameObject, 0f);
                    sound_break.PlayOneShot(sound_break.clip);
                    Debug.Log("Blue Delete");

                    //エフェクト
                    float wavesize = script.GetvolumeRate();
                    GameObject Wave = Instantiate(wave_B);
                    //Wave.transform.localScale = new Vector3(1 * wavesize, 1 * wavesize, 1 * wavesize);
                    Wave.transform.localScale = new Vector3(1 * obj.transform.localScale.x, 1 * obj.transform.localScale.y, 1 * obj.transform.localScale.z);
                    Wave.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                    countdown = 0.5f;   //時間リセット

                    if (scoreValue == 100)
                    {
                        blue_count_small += 1;
                    }
                    else if (scoreValue == 50)
                    {
                        blue_count += 1;
                    }
                    else if (scoreValue == 10)
                    {
                        blue_count_big += 1;
                    }

                    sm.AddScore(scoreValue);    //得点
                }
            }
        }
        
    }

    //破壊した個数を返す
    public static float red_normal()
    {
        return red_count;
    }

    public static float red_small()
    {
        return red_count_small;
    }

    public static float red_big()
    {
        return red_count_big;
    }

    public static float green_normal()
    {
        return green_count;
    }

    public static float green_small()
    {
        return green_count_small;
    }

    public static float green_big()
    {
        return green_count_big;
    }

    public static float blue_normal()
    {
        return blue_count;
    }

    public static float blue_small()
    {
        return blue_count_small;
    }

    public static float blue_big()
    {
        return blue_count_big;
    }
    
    public int GetSPfrag()
    {
        return flag;
    }
}
