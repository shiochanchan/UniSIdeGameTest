using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        // 画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        // ボタン(パネル)を非表示にする
        panel.SetActive(false);
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
        }
        else if (PlayerController._gameState == "playing")
        {
            // ゲーム中
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
