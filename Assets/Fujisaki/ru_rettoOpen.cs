using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ru_rettoOpen : MonoBehaviour
{
    //private static SettingsOpener instance;
    //private void Awake()
    //{
    //    // �C���X�^���X�����ɑ��݂���ꍇ�́A�V�������̂�j�����ďd���������
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    // ���̃C���X�^���X���B��ł��邱�Ƃ�ݒ肵�A�j������Ȃ��悤�ɂ���
    //    instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}
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
