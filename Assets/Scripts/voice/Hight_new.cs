using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hight_new : MonoBehaviour
{
    //private int qSamples = 1024;    //配列のサイズ
    private float threshold; //ピッチとして検出する最小の分布
    private float pitchValue;   //ピッチの周波数

    private float[] spectrum;   //FFTされたデータ
    private float fSample;  //サンプリング周波数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeSound();
    }

    void AnalyzeSound()
    {
        float[] spectrum = new float[256];
        //高速フーリエ変換を行う関数　
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);


        float maxV = 0;
        int maxN = 0;
        float threshold = 0.04f;
        for (int i = 0; i < spectrum.Length; i++)
        {
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i;
            }

        }

        float freqN = maxN;
        if (maxN > 0 && maxN < spectrum.Length - 1)
        {
            //隣のスペクトルも考慮する
            float dL = spectrum[maxN - 1] / spectrum[maxN];
            float dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }

        float pitchValue = freqN * (AudioSettings.outputSampleRate / 2) / spectrum.Length;
        Debug.Log(pitchValue);
    }
}
