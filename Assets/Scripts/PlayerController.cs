using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /// <summary> Rigidbody2D型の変数 </summary>
    Rigidbody2D rbody;
    /// <summary> 入力用変数 </summary>
    private float _axisH = 0.0f;
    /// <summary> 移動変数 </summary>
    public float _speed = 3.0f;
    /// <summary> ジャンプ変数 </summary>
    public float _jump = 9.0f;
    /// <summary> 着地できるレイヤー </summary>
    public LayerMask groundLayer;
    /// <summary> ジャンプ開始フラグ </summary>
    bool _gojump = false;
    /// <summary> 地面に立っているフラグ </summary>
    bool _onGround = false;

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
        // Playerをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    /// <summary>
    /// 速度更新
    /// </summary>
    private void FixedUpdate()
    {
        // 地上判定
        _onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        // 地面の上もしくは速度が0ではない
        if (_onGround || _axisH != 0)
        {
            // 速度を更新する
            rbody.velocity = new Vector2(_axisH * _speed, rbody.velocity.y);
        }
        // 地面の上でジャンプキーが押された
        if (_onGround && _gojump)
        {
            // ジャンプ可能
            Debug.Log("ジャンプ");
            Vector2 jumpPw = new Vector2(0, _jump);         //じゃんぷさせるためのメソッド
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬間的な力を加える
            _gojump = false;
        }
    }

    /// <summary>
    /// ジャンプするためのメソッド
    /// </summary>
    public void Jump()
    {
        _gojump = true;         //ジャンプフラグを建てる
        Debug.Log("ジャンプボタンをおした");
    }

}
