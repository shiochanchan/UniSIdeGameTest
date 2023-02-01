using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmicBlock : MonoBehaviour
{
    /// <summary> 自動落下検知距離 </summary>
    public float _length= 0;
    /// <summary> 落下後に削除するグラフ </summary>
    public bool _isDelete = false;

    /// <summary> 落下グラフ </summary>
    bool _isFell = false;
    /// <summary> フェードアウト時間 </summary>
    float _fadeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2Dの物理挙動を停止
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null )
        {
            // プレイヤーとの距離計算
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (_length >= d)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb.bodyType == RigidbodyType2D.Static)
                {
                    // RigidBody2Dの挙動を開始
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if (_isFell)
        {
            // 落下した
            // 透明値を変更してフェードアウトさせる
            // 前フレームの差分秒マイナス
            _fadeTime -= Time.deltaTime;
            // カラーを取り出す
            Color col = GetComponent<SpriteRenderer>().color;
            // 透明値の変更
            col.a = _fadeTime;
            // カラーの再設定
            GetComponent<SpriteRenderer>().color = col;
            if (_fadeTime <= 0.0f)
            {
                // 0以下(透明)になったら消す
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 接触開始
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDelete)
        {
           　// 落下フラグオン
            _isFell = true;
        }
    }
}
