using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovigBlock : MonoBehaviour
{
    /// <summary> X 移動距離 </summary>
    public float _moveX = 0.0f;
    /// <summary> Y 移動距離 </summary>
    public float _moveY = 0.0f;
    /// <summary> 時間 </summary>
    public float _times = 0.0f;
    /// <summary> 停止時間 </summary>
    public float _weight = 0.0f;
    /// <summary> 乗ったときに動くフラグ </summary>
    public bool _isMoveWhenOn = false;

    /// <summary> 動くフラグ </summary>
    public bool _isCanMove = true;
    /// <summary> 1フレームのX移動値 </summary>
    float perDX;
    /// <summary> 1フレームのY移動値 </summary>
    float perDY;
    /// <summary> 初期位置 </summary>
    Vector3 defPos;
    /// <summary> 反転フラグ </summary>
    bool _isReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置
        defPos = transform.position;
        // １フレームの移動時間取得
        float timestep = Time.fixedDeltaTime;
        // １フレームのX移動値
        perDX = _moveX / (1.0f / timestep * _times);
        // １フレームのY移動値
        perDY = _moveY / (1.0f / timestep * _times);

        if (_isMoveWhenOn)
        {
            // 乗った時に動くため初めは動かさない
            _isCanMove = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_isCanMove)
        {
            // 移動中
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if (_isReverse)
            {
                // 逆方向に移動中
                // 移動量がプラスで移動位置が初期より小さいまたは、量がマイナスで位置が初期より大きい
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX <= 0.0f && x >= defPos.x))
                {
                    // 移動量が＋で
                    endX = true;        // X方向の移動が終了
                }
                if ((perDY >= 0.0f && y <= defPos.y) || (perDY <= 0.0f && y >= defPos.y))
                {
                    endY = true;        // Y方向の移動終了
                }
                // 床を移動させる
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));
            }
            else
            {
                // 正方向移動中・・・
                // 移動量がプラスで位置が初期＋移動距離より大きいまたは、量がマイナスで位置が初期＋移動距離より小さい
                if ((perDX >= 0.0f && x >= defPos.x + _moveX) || (perDX < 0.0f && x <= defPos.x + _moveX))
                {
                    endX = true;       // X方向の移動終了
                }
                if ((perDY >= 0.0f && y >= defPos.y + _moveY) || (perDY < 0.0f && y <= defPos.y + _moveY))
                {
                    endY = true;       // Y方向の移動終了
                }
                // 床を移動させる
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
            }
            if (endX && endY)
            {
                // 移動終了
                if (_isReverse)
                {
                    // 正方向移動に戻る前に初期位置に戻す
                    transform.position = defPos;
                }
                // フラグを反転させる
                _isReverse = !_isReverse;
                // 移動フラグを下す
                _isCanMove = false;
                if (_isMoveWhenOn == false)
                {
                    // 乗ったときに動きフラグOFF
                    Invoke("Move", _weight);        // 移動フラグを建てる遅延実行
                }
            }
        }
    }

    /// <summary>
    /// 移動フラグを建てる
    /// </summary>
    public void Move()
    {
        _isCanMove = true;
    }

    /// <summary>
    /// 移動フラグを下ろす
    /// </summary>
    public void Stop()
    {
        _isCanMove = false;
    }

    /// <summary>
    /// 接触開始
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 接触したのがプレイヤーなら移動床の子に設定する
            collision.transform.SetParent(transform);
            if (_isMoveWhenOn)
            {
                // 乗ったときに動くフラグON
                _isCanMove = true;      // 移動フラグを建てる
            }
        }
    }

    /// <summary>
    /// 接触終了
    /// </summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 接触したのがプレイヤーなら移動床の子から外す
            collision.transform.SetParent(null);
        }
    }
}
