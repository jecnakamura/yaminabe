using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsOpener : MonoBehaviour
{
    private static SettingsOpener instance;
    private void Awake()
    {
        // インスタンスが既に存在する場合は、新しいものを破棄して重複を避ける
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // このインスタンスが唯一であることを設定し、破棄されないようにする
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Tabキーまたはコントローラーボタンで設定シーンに遷移
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenSettingsScene();
        }
        // エスケープキーが押されたとき
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ゲームを終了
            Application.Quit();

            // エディタ内で動作を確認する場合
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    private void OpenSettingsScene()
    {
        // SettingsSceneが既にロードされているかを確認
        Scene settingsScene = SceneManager.GetSceneByName("SettingsScene");
        if (settingsScene.isLoaded)
        {
            // 既にSettingsSceneがロードされている場合、何もしない
            return;
        }

        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }
}
