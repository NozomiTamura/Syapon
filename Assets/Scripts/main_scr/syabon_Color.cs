using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syabon_Color : MonoBehaviour
{
    public ParticleSystem main;

    float h;

    // Start is called before the first frame update
    void Start()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        h = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //色変化
        main.startColor = UnityEngine.Color.HSVToRGB(h/360, 0.61f, 1.0f);
        //main.material.SetColor("_EmissionColor", main.startColor);
        Debug.Log(h);
        h++;
        if (h > 360) {
            h = 0.0f;  //リセット
        }
    }
}
