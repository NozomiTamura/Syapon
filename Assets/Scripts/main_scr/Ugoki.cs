using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ugoki : MonoBehaviour
{
    float x,y;
    float radius;
    Vector3 pos;

    public GameObject sp;
    float h;

    //HP実装
    int syabon_hp = 30;

    // Start is called before the first frame update
    void Start()
    {
        //初期設定
        radius = 7;
        x = 0;
        y = 0;

        //初期場所を取得
        //Vector3 pos = this.gameObject.transform.position;
        pos.x = -25f;
        pos.y = 0f;
        pos.z = 0f;

        h = 120.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pos.x < 25)
        {
            pos.x += x / 4000;
            pos.y = radius * Mathf.Sin(y / 30);
            this.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
            x++;
            y++;
        }
        else {
            //初期化
            radius = 7;
            x = 0;
            y = 0;

            pos.x = -25f;
            pos.y = 0f;
            pos.z = 0f;
        }

        //色変化
        var color = UnityEngine.Color.HSVToRGB(h / 360, 1.0f, 1.0f);
        GetComponent<Renderer>().material.color = color;
        if (h < 0)
        {
            h = 120.0f;  //リセット
        }
    }

    void OnTriggerStay(Collider obj) //OnCollisionStay(Collision obj)
    {
        //プレイヤーが接触したときに色が変化（hが減る）
        if (obj.gameObject.tag == "Player")
        {
            syabon_hp--;
            if (syabon_hp % 3 == 0)
            {
                h -= 12;
            }
        }
    }
}
