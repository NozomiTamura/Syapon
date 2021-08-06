using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class count : MonoBehaviour
{
    private Text countText;
    //private Text redText, greenText, blueText;
    public Text redText, greenText, blueText;
    float red_big, red, red_small;
    float green_big, green, green_small;
    float blue_big, blue, blue_small;

    //合計の数
    public Text allText;
    float big, normal, small;

    //音
    // public AudioClip sound;
    // AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //赤（黄色）
        red_big = Player_Script.red_big();
        red = Player_Script.red_normal();
        red_small = Player_Script.red_small();
        //緑
        green_big = Player_Script.green_big();
        green = Player_Script.green_normal();
        green_small = Player_Script.green_small();
        //青
        blue_big = Player_Script.blue_big();
        blue = Player_Script.blue_normal();
        blue_small = Player_Script.blue_small();

        //合計の数
        big = red_big + green_big + blue_big;
        normal = red + green + blue;
        small = red_small + green_small + blue_small;

        //赤
        if (red_big > 9)
        {   redText.text = "大:" + red_big.ToString();}
        else{   redText.text = "大: " + red_big.ToString();}
        if (red > 9)
        {    redText.text += "  中:" + red.ToString();}
        else{    redText.text += "  中: " + red.ToString();}
        if (red_small > 9)
        {   redText.text += "  小:" + red_small.ToString();}
        else{   redText.text += "  小: " + red_small.ToString();}

        //緑
        if (green_big > 9)
        {   greenText.text = "大:" + green_big.ToString();}
        else{   greenText.text = "大: " + green_big.ToString();}
        if (green > 9)
        {   greenText.text += "  中:" + green.ToString();}
        else{   greenText.text += "  中: " + green.ToString();}
        if (green_small > 9)
        {   greenText.text += "  小:" + green_small.ToString();}
        else{   greenText.text += "  小: " + green_small.ToString();}

        //青
        if (blue_big > 9)
        {   blueText.text = "大:" + blue_big.ToString();}
        else{   blueText.text = "大: " + blue_big.ToString();}
        if (blue > 9)
        {   blueText.text += "  中:" + blue.ToString();}
        else{   blueText.text += "  中: " + blue.ToString();}
        if (blue_small > 9)
        {   blueText.text += "  小:" + blue_small.ToString();}
        else{   blueText.text += "  小: " + blue_small.ToString();}

        //ALL
        //if (big > 9)
        //{ allText.text = "big:" + big.ToString(); }
        //else { allText.text = "big: " + big.ToString(); }
        //if (normal > 9)
        //{ allText.text += " normal:" + normal.ToString(); }
        //else { allText.text += " normal: " + normal.ToString(); }
        //if (small > 9)
        //{ allText.text += " small:" + small.ToString(); }
        //else { allText.text += " small: " + small.ToString(); }

        //音
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //表示させる
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     redText.gameObject.SetActive(true);
        //     audioSource.PlayOneShot(sound);  //音を再生
        // }
    }
}
