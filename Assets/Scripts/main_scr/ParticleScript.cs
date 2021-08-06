using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private Player_Script player_script;
    //　パーティクルシステム
    private ParticleSystem ps;
    //　ScaleUp用の経過時間
    private float elapsedScaleUpTime = 0f;
    //　Scaleを大きくする間隔時間
    [SerializeField]
    private float scaleUpTime = 0.03f;
    //　ScaleUpする割合
    [SerializeField]
    private float scaleUpParam = 0.1f;
    //　パーティクル削除用の経過時間
    private float elapsedDeleteTime = 0f;
    //　パーティクルを削除するまでの時間
    [SerializeField]
    private float deleteTime = 5f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        player_script = GameObject.Find("Player").GetComponent<Player_Script>();
        ps.trigger.SetCollider(0, player_script.transform);
        //Color color = gameObject.GetComponent<Renderer>().material.color;
        //color.a -= 0.1f;
        //gameObject.GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedScaleUpTime += Time.deltaTime;
        elapsedDeleteTime += Time.deltaTime;


        if (elapsedDeleteTime >= deleteTime)
        {
            Destroy(gameObject);
        }

        if (elapsedScaleUpTime > scaleUpTime)
        {
            transform.localScale += new Vector3(scaleUpParam, scaleUpParam, scaleUpParam);
            elapsedScaleUpTime = 0f;
        }
    }

}