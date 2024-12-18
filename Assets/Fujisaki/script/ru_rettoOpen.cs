using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ru_rettoOpen : MonoBehaviour
{
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
