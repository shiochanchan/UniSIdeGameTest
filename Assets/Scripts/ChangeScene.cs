using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;       // シーン切り替え

public class ChangeScene : MonoBehaviour
{
    public string _sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// シーンを読み込む
    /// </summary>
    public void Load()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
