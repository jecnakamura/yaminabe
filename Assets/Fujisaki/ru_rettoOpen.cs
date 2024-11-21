using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ru_rettoOpen : MonoBehaviour
{
    //private static SettingsOpener instance;
    //private void Awake()
    //{
    //    // インスタンスが既に存在する場合は、新しいものを破棄して重複を避ける
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    // このインスタンスが唯一であることを設定し、破棄されないようにする
    //    instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}
    public void OnClick()
    {
        OpenSettingsScene();
    }
    private void OpenSettingsScene()
    {
        // SettingsSceneが既にロードされているかを確認
        Scene settingsScene = SceneManager.GetSceneByName("Ruretto");
        if (settingsScene.isLoaded)
        {
            // 既にSettingsSceneがロードされている場合、何もしない
            return;
        }

        SceneManager.LoadScene("Ruretto", LoadSceneMode.Additive);
    }
}
