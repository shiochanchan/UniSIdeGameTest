using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    /// <summary> 発生させるPrefabデータ </summary>
    public GameObject objPrefab;
    /// <summary> 遅延時間 </summary>
    public float delayTime = 3.0f;
    /// <summary> 発射ベクトルX </summary>
    public float fireSpeedX = -4.0f;
    /// <summary> 発射ベクトルY </summary>
    public float fireSpeedY = 0.0f;
    public float length = 8.0f;

    GameObject player;
    GameObject gateObj;
    float _passedTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 発射オブジェクトを取得
        Transform tr = transform.Find("gate");
        gateObj = tr.gameObject;
        // プレイヤー
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 発車時間判定
        _passedTimes += Time.deltaTime;
        if (CheckLength(player.transform.position))
        {
            if (_passedTimes > delayTime)
            {
                // 発射
                _passedTimes = 0;
                // 発射位置
                Vector3 pos = new Vector3(gateObj.transform.position.x, gateObj.transform.position.y, transform.position.z);
                // Prefab から GameObjectを作る
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                // 発射方向
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2 (fireSpeedX, fireSpeedY);
                rbody.AddForce (v, ForceMode2D.Impulse);
            }
        }
    }

    private bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= 0)
        {
            ret = true;
        }
        return ret;
    }
}
