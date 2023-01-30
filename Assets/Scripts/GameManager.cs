using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;       // UIを使用する

public class GameManager : MonoBehaviour
{
    /// <summary> 画像オブジェクト </summary>
    public GameObject mainImage;
    /// <summary> ゲームオーバー用画像 </summary>
    public Sprite gameOverSpr;
    /// <summary> クリア画像 </summary>
    public Sprite gameClearSpr;
    /// <summary> パネル </summary>
    public GameObject panel;
    /// <summary> restartボタン </summary>
    public GameObject restartButton;
    /// <summary> クリア画像 </summary>
    public GameObject nextButton;

    /// <summary> 画像を表示するためのイメージコンポーネント </summary>
    Image titleImage;

    // +++時間制限の追加+++
    /// <summary> 時間表現イメージ </summary>
    public GameObject timeBar;
    /// <summary> 時間テキスト </summary>
    public GameObject timeText;
    /// <summary> タイムコントローラー </summary>
    TimeController timeCnt;

    // Start is called before the first frame update
    void Start()
    {
        // 画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        // ボタン(パネル)を非表示にする
        panel.SetActive(false);

        // +++時間制限追加+++
        /// <summary> TimeControllerの取得 </summary>
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt._gameTime == 0.0f)
            {
                // 時間制限なしなら隠す
                timeBar.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController._gameState == "gameclear")
        {
            // ゲームクリア画面
            mainImage.SetActive(true);      // 画像を表示する
            panel.SetActive(true);          // ボタンを表示する
            // restartボタンを無効化
            Button button = restartButton.GetComponent<Button>();
            button.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;      // 画像を設定する
            PlayerController._gameState = "gameend";
            // +++時間制限の追加+++
            if (timeCnt != null)
            {
                // 時間カウント停止
                timeCnt._isTimeOver = true;
            }
        }
        else if (PlayerController._gameState == "gameover")
        {
            // ゲームオーバー
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button button = nextButton.GetComponent<Button>();
            button.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;       // 画像を設定
            PlayerController._gameState = "gameend";
            // +++時間制限の追加+++
            if (timeCnt != null)
            {
                // 時間カウント停止
                timeCnt._isTimeOver = true;
            }
        }
        else if (PlayerController._gameState == "playing")
        {
            // ゲーム中
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // プレイヤーコントローラの取得
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            // +++時間制限の追加+++
            // タイムを更新する
            if (timeCnt != null)
            {
                if (timeCnt._gameTime > 0.0f)
                {
                    // 整数に代入することで少数を切り捨てる
                    int _time = (int)timeCnt._displayTime;
                    // タイム更新
                    timeText.GetComponent<TextMeshProUGUI>().text = _time.ToString();
                    // タイムオーバー
                    if(_time == 0)
                    {
                        // 時間0でゲームオーバーになる
                        playerCnt.GameOver();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 画像を非表示にする
    /// </summary>
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
