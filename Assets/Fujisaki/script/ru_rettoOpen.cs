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
        // SettingsScene�����Ƀ��[�h����Ă��邩���m�F
        Scene settingsScene = SceneManager.GetSceneByName("Ruretto");
        if (settingsScene.isLoaded)
        {
            // ����SettingsScene�����[�h����Ă���ꍇ�A�������Ȃ�
            return;
        }

        SceneManager.LoadScene("Ruretto", LoadSceneMode.Additive);
    }
}
