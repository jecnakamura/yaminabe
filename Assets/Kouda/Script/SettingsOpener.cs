using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsOpener : MonoBehaviour
{
    private static SettingsOpener instance;
    private void Awake()
    {
        // �C���X�^���X�����ɑ��݂���ꍇ�́A�V�������̂�j�����ďd���������
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // ���̃C���X�^���X���B��ł��邱�Ƃ�ݒ肵�A�j������Ȃ��悤�ɂ���
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Tab�L�[�܂��̓R���g���[���[�{�^���Őݒ�V�[���ɑJ��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenSettingsScene();
        }
        // �G�X�P�[�v�L�[�������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �Q�[�����I��
            Application.Quit();

            // �G�f�B�^���œ�����m�F����ꍇ
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    private void OpenSettingsScene()
    {
        // SettingsScene�����Ƀ��[�h����Ă��邩���m�F
        Scene settingsScene = SceneManager.GetSceneByName("SettingsScene");
        if (settingsScene.isLoaded)
        {
            // ����SettingsScene�����[�h����Ă���ꍇ�A�������Ȃ�
            return;
        }

        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }
}
