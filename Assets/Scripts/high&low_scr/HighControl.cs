using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighControl : MonoBehaviour
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
    
    int maxIndex = 0;
    float maxValue = 0.0f;

    //高い音
    public int high=0;
    int count = 0;

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
        //Debug.Log(freq2);
        

        if (volumeRate < 0.002)
        {
            //音が出てない時は反応しない
        }else {
            high = freq2;   //音が出ている時に周波数取得
        }
    }

    public int GetMaxIndext()
    {
        return maxIndex;
    }

    public float GetvolumeRate()
    {
        return volumeRate * 50;
    }

    public int High() {
        return high;
    }
}