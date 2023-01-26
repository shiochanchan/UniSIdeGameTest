using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /// <summary> Rigidbody2D型の変数 </summary>
    public Rigidbody2D rbody;
    /// <summary> 入力用変数 </summary>
    private float _axisH = 0.0f;
    /// <summary> 移動変数 </summary>
    public float _speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2Dをとる
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //　水平方向の入力を確認する
        _axisH = Input.GetAxisRaw("Horizontal");
        //　向きの調整
        if (_axisH > 0.0f )
        {
            // 右方向の向き
            Debug.Log("右に移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if (_axisH < 0.0f )
        {
            // 左に移動
            Debug.Log("左に移動");
            // 左右に反転させる
            transform.localScale = new Vector2(-1, 1);
        }
    }

    /// <summary>
    /// 速度更新
    /// </summary>
    private void FixedUpdate()
    {
        /// 速度を更新する
        rbody.velocity = new Vector2(_axisH * _speed, rbody.velocity.y);
    }

}
