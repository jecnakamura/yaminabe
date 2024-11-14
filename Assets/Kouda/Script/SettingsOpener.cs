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
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("OpenSettings"))
        {
            OpenSettingsScene();
        }
    }

    private void OpenSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }
}
