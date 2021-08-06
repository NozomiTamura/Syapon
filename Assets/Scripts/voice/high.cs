using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class high : MonoBehaviour
{
    private readonly int SampleNum = (2 << 9); // サンプリング数は2のN乗(N=5-12)
    [SerializeField, Range(0f, 1000f)] float m_gain = 200f; // 倍率
    AudioSource m_source;
    LineRenderer m_lineRenderer;
    Vector3 m_sttPos;
    Vector3 m_endPos;
    float[] currentValues;

    float volumeRate;

    GameObject ball;
    Vector3 stey = new Vector3(0, 0, 0);

    public int resolution = 1024;

    int maxIndex = 0;
    float maxValue = 0.0f;

    //声の色変更関連
    private ColorChange cc;
    GameObject color;
    

    //高い声
    float aaa, aaa3;

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
        

        if (volumeRate * 100 > 1)
        {
            
        }
        else
        {
            
        }

        for (int i = 0; i < levelCount; i++)
        {
            positions[i] = m_sttPos + (m_endPos - m_sttPos) * (float)i / (float)(levelCount - 1);
            positions[i].y += currentValues[i] * m_gain;
        }
        m_lineRenderer.positionCount = levelCount;
        m_lineRenderer.SetPositions(positions);


        float[] spectrum = new float[256];
        //高速フーリエ変換を行う関数　
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);


        float maxIndex2 = 0;
        float maxValue2 = 0.0f;

        for (int i = 0; i < spectrum.Length; i++)
        {
            float val2 = spectrum[i];
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
        float freq2 = maxIndex2 * AudioSettings.outputSampleRate / 2 / spectrum.Length;

        //Debug.Log(volumeRate);

        if (volumeRate < 0.04)
        {   //音が出てない時は白に戻す
            //色の初期化
            cc.White();

        }
        else
        {
            Debug.Log(freq2);
            //Debug.Log(maxIndex2);

            //高い音を設定する
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                aaa = freq2;
                Debug.Log(aaa);
            }
            //低い音
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //int aaa = freq2;
                aaa3 = freq2;
                Debug.Log(aaa3);
            }

            //周波数　＝　音の高さで判別
            if (freq2 >= aaa - 100)
            {
                cc.Red();
                
            }
            if (freq2 < aaa - 100 && freq2 >= aaa3 + 100)
            {
                cc.Green();
                
            }
            if (freq2 < aaa3 + 100)
            {
                cc.Blue();
                
            }
        }

    }

    //public int High()
    //{
    //    return high;
    //}

    //ヘルツから音階への変換
    float ConvertHertzToScale(float hertz) {
        if (hertz == 0) return 0.0f;
        else return (12.0f * Mathf.Log(hertz / 110.0f) / Mathf.Log(2.0f));
    }

    //数値音階から文字音階への変換
    string ConvertScaleToString(float scale) {
        //１２音階の何倍の精度で音階を見るか
        int precision = 2;

        //今の場合だと、mod24が0ならA、1ならA#の間、2ならA#...
        int s = (int)scale;
        if (scale - s >= 0.5) s += 1;   //四捨五入
        s *= precision;

        int smod = s % (12 * precision);    //音階
        int soct = s / (12 * precision);    //オクターブ

        string value;   //返す値

        if (smod == 0) value = "A";
        else if (smod == 1) value = "A+";
        else if (smod == 2) value = "A#";
        else if (smod == 3) value = "A#+";
        else if (smod == 4) value = "B";
        else if (smod == 5) value = "B+";
        else if (smod == 6) value = "C";
        else if (smod == 7) value = "C+";
        else if (smod == 8) value = "C#";
        else if (smod == 9) value = "C#+";
        else if (smod == 10) value = "D";
        else if (smod == 11) value = "D+";
        else if (smod == 12) value = "D#";
        else if (smod == 13) value = "D#+";
        else if (smod == 14) value = "E";
        else if (smod == 15) value = "E+";
        else if (smod == 16) value = "F";
        else if (smod == 17) value = "F+";
        else if (smod == 18) value = "F#";
        else if (smod == 19) value = "F#+";
        else if (smod == 20) value = "G";
        else if (smod == 21) value = "G+";
        else if (smod == 22) value = "G#";
        else value = "G#+";
        value += soct + 1;

        return value;
    }
}