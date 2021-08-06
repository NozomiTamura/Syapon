using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamenControl : MonoBehaviour
{
    public Text redText, greenText, blueText ,scoreText;
    public Text redText2, greenText2, blueText2;
    //public GameObject tx_goukei;

    //合計の数
    public Text allText;

    float cd = 5.0f;
    int flag = 0;

    public AudioClip sound;
    AudioSource audioSource;
    


    // Start is called before the first frame update
    void Start()
    {
        //redText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //greenText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //blueText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //scoreText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //allText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        redText.gameObject.SetActive(false);
        greenText.gameObject.SetActive(false);
        blueText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        allText.gameObject.SetActive(false);

        redText2.gameObject.SetActive(false);
        greenText2.gameObject.SetActive(false);
        blueText2.gameObject.SetActive(false);

        flag = 0;
        audioSource = GetComponent<AudioSource>();

        cd = 5.0f;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if(cd<=4 && flag==0){
            //tx_r.SetActive(true);
            //redText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            redText.gameObject.SetActive(true);
            redText2.gameObject.SetActive(true);
            audioSource.PlayOneShot(sound);  //音を再生
            flag++;
        }
        if(cd<=3&& flag==1){
            //tx_g.SetActive(true);
            //greenText.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            // greenText.gameObject.SetActive(true);
            // greenText2.gameObject.SetActive(true);
            // audioSource.PlayOneShot(sound);  //音を再生
            blueText.gameObject.SetActive(true);
            blueText2.gameObject.SetActive(true);
            audioSource.PlayOneShot(sound);  //音を再生

            flag++;
        }
        if(cd<=2&& flag==2){
            //tx_b.SetActive(true);
            //blueText.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            scoreText.gameObject.SetActive(true);
            audioSource.PlayOneShot(sound);  //音を再生
            
            flag++;
        }
        if(cd<=1&& flag==3){
            flag++;

            //
            //allText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            //allText.gameObject.SetActive(true);
            //audioSource.PlayOneShot(sound);  //音を再生
            //flag++;
        }
        if(cd<=0&& flag==4){
            //score.SetActive(true);
            //scoreText.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            //scoreText.gameObject.SetActive(true);
            //audioSource.PlayOneShot(sound);  //音を再生
            //flag++;
        }


    }
}
