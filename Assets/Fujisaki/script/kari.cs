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
        // SettingsScene�����Ƀ��[�h����Ă��邩���m�F
        Scene settingsScene = SceneManager.GetSceneByName("Yasai1RurettoScene");
        if (settingsScene.isLoaded)
        {
            // ����SettingsScene�����[�h����Ă���ꍇ�A�������Ȃ�
            return;
        }

        SceneManager.LoadScene("Yasai1RurettoScene", LoadSceneMode.Additive);
    }
}
