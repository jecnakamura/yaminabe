using UnityEngine;
using UnityEngine.SceneManagement;

public class kari : MonoBehaviour
{
    public void OnClick()
    {
        OpenSettingsScene();
    }
    private void OpenSettingsScene()
    {
        // SettingsSceneが既にロードされているかを確認
        Scene settingsScene = SceneManager.GetSceneByName("Yasai1RurettoScene");
        if (settingsScene.isLoaded)
        {
            // 既にSettingsSceneがロードされている場合、何もしない
            return;
        }

        SceneManager.LoadScene("Yasai1RurettoScene", LoadSceneMode.Additive);
    }
}
