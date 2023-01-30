using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    /// <summary> true=時間をカウントダウン計算する </summary>
    public bool _isCountDown = true;
    /// <summary> ゲームの最大時間 </summary>
    public float _gameTime = 0;
    /// <summary> true = タイマー停止 </summary>
    public bool _isTimeOver = false;
    /// <summary> 表示切替 </summary>
    public float _displayTime = 0;
    /// <summary> 現在時間 </summary>
    float _times = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (_isCountDown)
        {
            // カウントダウン
            _displayTime = _gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTimeOver == false)
        {
            _times += Time.deltaTime;
            if (_isCountDown)
            {
                // カウントダウン
                _displayTime = _gameTime - _times;
                if(_displayTime <= 0.0f)
                {
                    _displayTime = 0.0f;
                    _isTimeOver = true;
                }
            }
            else
            {
                // カウントアップ
                _displayTime = _times;
                if(_displayTime >= 0.0f)
                {
                    _displayTime = _gameTime;
                    _isTimeOver = true;
                }
            }
            Debug.Log("TIMES:" + _displayTime);
        }
    }
}
