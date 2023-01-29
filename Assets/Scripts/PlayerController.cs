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

    /// <summary>
    /// アニメーション対応
    /// </summary>
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerClear";
    public string deadAnime = "PlayerGameOver";
    string _nowAnime = "";
    string _oldAnime = "";

    /// <summary> ゲームの状態 </summary>
    public static string _gameState = "playing";

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2Dをとる
        rbody = GetComponent<Rigidbody2D>();
        // Animatorを取ってくる
        animator = GetComponent<Animator>();
        _nowAnime = stopAnime;
        _oldAnime = stopAnime;
        // ゲーム中にする
        _gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム中以外の判定でメソッド内の以下を実行しない
        if(_gameState != "playing")
        {
            return;
        }
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
    void FixedUpdate()
    {
        // ゲーム中以外はメソッド内の以下の判定を実行しない
        if (_gameState != "playing")
        {
            return;
        }
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
        // 地上にいるとき
        if (_onGround)
        {
            if (_axisH == 0)
            {
                _nowAnime = stopAnime;
            }
            else
            {
                _nowAnime = moveAnime;
            }
        }
        else
        {
            _nowAnime = jumpAnime;
        }

        // アニメーション再生
        if(_nowAnime != _oldAnime)
        {
            _oldAnime = _nowAnime;
            animator.Play(_nowAnime);
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

    /// <summary>
    /// 接触開始
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            // ゴールメソッドを呼ぶ
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            // ゲームオーバーメソッドを呼ぶ
            GameOver();
        }
    }

    /// <summary>
    /// ゴールメソッド
    /// </summary>
    public void Goal()
    {
        animator.Play(goalAnime);

        _gameState = "gameclear";
        // ゲーム停止メソッドを呼ぶ
        GameStop();
    }

    /// <summary>
    /// ゲームオーバーメソッド
    /// </summary>
    public void GameOver()
    {
        animator.Play(deadAnime);

        Debug.Log("しんだ");
        _gameState = "gameover";
        // ゲーム停止メソッドを呼ぶ
        GameStop();
        // ゲームオーバー演出
        // プレイヤーの当たり判定を消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        // プレイヤーを少し上に上げる
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    /// <summary>
    /// ゲーム停止メソッド
    /// </summary>
    void GameStop()
    {
        // Rigidbody2Dをとってくる
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        // 速度0で強制停止
        rbody.velocity = new Vector2(0, 0);
    }
}
