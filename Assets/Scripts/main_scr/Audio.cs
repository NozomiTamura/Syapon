using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource sound_red;
    public AudioSource sound_green;
    public AudioSource sound_blue;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSourceコンポーネントを取得し、変数に格納
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound_red = audioSources[0];
        sound_green = audioSources[1];
        sound_blue = audioSources[2];
    }

    // Update is called once per frame
    void Update()
    {
        //消えた時、音を鳴らす
        //if (Input.GetKeyDown(KeyCode.LeftArrow)) {
        //    sound_red.PlayOneShot(sound_red.clip);
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    sound_green.PlayOneShot(sound_green.clip);
        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    sound_blue.PlayOneShot(sound_blue.clip);
        //}
    }
}
