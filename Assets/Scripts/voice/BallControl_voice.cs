using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl_voice : MonoBehaviour
{
    private readonly int SampleNum = (2 << 9); // サンプリング数は2のN乗(N=5-12)
    [SerializeField, Range(0f, 1000f)] float m_gain = 200f; // 倍率
    AudioSource m_source;
    LineRenderer m_lineRenderer;
    Vector3 m_sttPos;
    Vector3 m_endPos;
    float[] currentValues;
    //int flag = 0;
    //float volume_max = 0;
    float volumeRate;

    GameObject ball;
    Vector3 stey = new Vector3(0, 0, 0);

    public int resolution = 1024;
    //public Transform lowMeter, midMeter, highMeter;
    //public float lowFreqThreshold = 500, midFreqThreshold = 1000, highFreqThreshold = 44100;
    //public float lowEnhance = 1f, midEnhance = 10f, highEnhance = 100f;

    int maxIndex = 0;
    float maxValue = 0.0f;

    // //プレハブ
    // public GameObject wave_B;
    // public GameObject wave_R;
    // public GameObject wave_G;
    // //プレハブ判定用
    // int flag_R = 0;
    // int flag_B = 0;
    // //----------

    //声の色変更関連
    private ColorChange cc;
    GameObject color;
    int aa = 0;

    // Use this for initialization
    void Start()
    {
        m_source = GetComponent<AudioSource>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_sttPos = m_lineRenderer.GetPosition(0);
        m_endPos = m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 1);
        currentValues = new float[SampleNum];
        if ((m_source != null) && (Microphone.devices.Length > 0)) // オーディオソースとマイクがある
        {
            if (m_source.clip == null) // クリップがなければマイクにする
            {
                string devName = Microphone.devices[0]; // 複数見つかってもとりあえず0番目のマイクを使用
                int minFreq, maxFreq;
                Microphone.GetDeviceCaps(devName, out minFreq, out maxFreq); // 最大最小サンプリング数を得る
                int ms = minFreq / SampleNum; // サンプリング時間を適切に取る
                m_source.loop = true; // ループにする
                m_source.clip = Microphone.Start(devName, true, ms, minFreq); // clipをマイクに設定
                while (!(Microphone.GetPosition(devName) > 0)) { } // きちんと値をとるために待つ
                Microphone.GetPosition(null);
                m_source.Play();
            }
        }
        //色関係
        color = GameObject.Find("ColorChange");
        cc = color.GetComponent<ColorChange>();
    }

    // Update is called once per frame
    void Update()
    {

        m_source.GetSpectrumData(currentValues, 0, FFTWindow.Hamming);
        int levelCount = currentValues.Length / 8; // 高周波数帯は取らない
        Vector3[] positions = new Vector3[levelCount];
        // --------------------------最大周波数--------------------------------
        maxIndex = 0;
        maxValue = 0.0f;
        // maxValue が最も大きい周波数成分の値で、
        // maxIndex がそのインデックス。欲しいのはこっち。
        for (int i = 0; i < currentValues.Length; i++)
        {
            var val = currentValues[i];
            if (val > maxValue)
            {
                maxValue = val;
                maxIndex = i;
            }
        }


        //---------------------------音量-----------------------------
        float sum = 0f;

        for (int i = 0; i < currentValues.Length; ++i)
        {
            sum += currentValues[i]; // データ（周波数帯ごとのパワー）を足す
        }


        // データ数で割ったものに倍率をかけて音量とする
        volumeRate = Mathf.Clamp01(sum * m_gain / (float)currentValues.Length);
        //Debug.Log(volumeRate*100);

        //GameObject Sphere = GameObject.FindGameObjectWithTag("Player");


        if (volumeRate * 100 > 1)
        {
            //Sphere.transform.localScale = new Vector3(100 * volumeRate * 50, 100 * volumeRate * 50, 100 * volumeRate * 50);
            // //Debug.Log(maxIndex);
            // //空き箱最頻値22−24
            // //空き缶最頻値35-37
            // if (maxIndex > 21 && maxIndex < 25)
            // {
            //     Debug.Log("空き箱");
            //     Sphere.transform.localScale = new Vector3(100 * volumeRate * 25, 100 * volumeRate * 25, 100 * volumeRate * 25);
            //     flag_R = 1;
            //     flag_B = 0;
            // }
            // else if (maxIndex > 34 && maxIndex < 38)
            // {
            //     Debug.Log("空き缶");
            //     Sphere.transform.localScale = new Vector3(100 * volumeRate * 50, 100 * volumeRate * 50, 100 * volumeRate * 50);
            //     flag_B = 1;
            //     flag_R = 0;
            // }
            // //-------------------------
            // if (volumeRate * 100 > 3)
            // {
            //     flag = 1;

            // }
            // if (volume_max < volumeRate * 50)
            // {
            //     volume_max = volumeRate * 50;
            //}
        }
        else
        {
            //Sphere.transform.localScale = new Vector3(1, 1, 1);
            // if (flag == 1)
            // {
            //     Sphere.transform.localScale = new Vector3(100, 100, 100);
            //     //ウェーブ生成　　缶青　箱赤
            //     if(flag_B == 1)
            //     {
            //         GameObject Wave = Instantiate(wave_B);
            //         Wave.transform.localScale = new Vector3(1 * volume_max, 1 * volume_max, 1 * volume_max);
            //     }
            //     else if (flag_R == 1)
            //     {
            //         GameObject Wave = Instantiate(wave_R);
            //         Wave.transform.localScale = new Vector3(1 * volume_max/2, 1 * volume_max / 2, 1 * volume_max / 2);
            //     }

            //     //リセット
            //     flag = 0;
            //     volume_max = 0;
            //     flag_R = 0;
            //     flag_B = 0;

            // }
        }

        //Debug.Log(volumeRate);

        //-------------------------------------------------------
        // var deltaFreq = AudioSettings.outputSampleRate / currentValues.Length;
        // float low = 0f, mid = 0f, high = 0f;

        // for (var i = 0; i < currentValues.Length; ++i)
        // {
        //     var freq = deltaFreq * i;
        //     if (freq <= lowFreqThreshold) low += currentValues[i];
        //     else if (freq <= midFreqThreshold) mid += currentValues[i];
        //     else if (freq <= highFreqThreshold) high += currentValues[i];
        // }

        // low *= lowEnhance;
        // mid *= midEnhance;
        // high *= highEnhance;

        // lowMeter.localScale = new Vector3(lowMeter.localScale.x, low, lowMeter.localScale.z);
        // midMeter.localScale = new Vector3(midMeter.localScale.x, mid, midMeter.localScale.z);
        // highMeter.localScale = new Vector3(highMeter.localScale.x, high, highMeter.localScale.z);

        //----------------------------------------------------------


        for (int i = 0; i < levelCount; i++)
        {
            positions[i] = m_sttPos + (m_endPos - m_sttPos) * (float)i / (float)(levelCount - 1);
            positions[i].y += currentValues[i] * m_gain;
        }
        m_lineRenderer.positionCount = levelCount;
        m_lineRenderer.SetPositions(positions);



        //周波数の解析
        float[] spectrum = new float[256];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);


        var maxIndex2 = 0;
        var maxValue2 = 0.0f;
        for (int i = 0; i < spectrum.Length; i++)
        {
            var val2 = spectrum[i];
            //最大
            if (val2 > maxValue2)
            {
                maxValue2 = val2;
                maxIndex2 = i;
            }

        }
        // maxValue2 が最も大きい周波数成分の値で、
        // maxIndex2 がそのインデックス。欲しいのはこっち。


        //周波数
        var freq2 = maxIndex2 * AudioSettings.outputSampleRate / 2 / spectrum.Length;

        //Debug.Log(volumeRate);

        if (volumeRate < 0.05)
        {   //音が出てない時は白に戻す
            //色の初期化
            cc.White();

        }else {
            //Debug.Log(freq2);
            //Debug.Log(maxIndex2);


            //周波数　＝　音の高さで判別
            if (freq2 >= 1000)
            {
                cc.Red();
                aa = 0;
            }
            else if (freq2 < 1000 && freq2 >= 500)
            {
                cc.Green();
                aa = 1;
            }
            else if (freq2 < 500 && freq2 > 10)
            {
                cc.Blue();
                aa = 2;
            }
        }

        
        //fDebug.Log(aa);
    }



    public int GetMaxIndext()
    {
        return maxIndex;
    }

    public int Freq() {
        return aa;
    }

    public float GetvolumeRate()
    {
        return volumeRate * 50;
    }
}