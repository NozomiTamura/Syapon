using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //playerを取得
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //色変化
        //Color();
    }

    //色変化
    public void Color() {
        if (Input.GetKeyDown(KeyCode.R))    //赤
        {
            Material mat = Resources.Load("red") as Material;
            player.GetComponent<Renderer>().material = mat;
        }
        else if (Input.GetKeyDown(KeyCode.G))   //青
        {
            Material mat = Resources.Load("green") as Material;
            player.GetComponent<Renderer>().material = mat;
        }
        else if (Input.GetKeyDown(KeyCode.B))   //緑
        {
            Material mat = Resources.Load("blue") as Material;
            player.GetComponent<Renderer>().material = mat;
        }
    }

    public void Red()
    {
        Material mat = Resources.Load("red") as Material;
        player.GetComponent<Renderer>().material = mat;
    }

    public void Green()
    {
        Material mat = Resources.Load("green") as Material;
        player.GetComponent<Renderer>().material = mat;
    }

    public void Blue()
    {
        Material mat = Resources.Load("blue") as Material;
        player.GetComponent<Renderer>().material = mat;
    }

    public void White()
    {
        Material mat = Resources.Load("wakka") as Material;
        player.GetComponent<Renderer>().material = mat;
    }
}
