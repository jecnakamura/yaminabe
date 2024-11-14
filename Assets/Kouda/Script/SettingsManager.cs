using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;

    private Resolution[] resolutions;

    private void Start()
    {
        // ���ʃX���C�_�[�̏�����
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // ��ʃT�C�Y�I���̏�����
        InitializeResolutionOptions();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // �{�^���̋@�\�ݒ�
        closeButton.onClick.AddListener(CloseSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private void InitializeResolutionOptions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void CloseSettings()
    {
        // ���̃V�[���ɖ߂�
        SceneManager.UnloadSceneAsync("SettingsScene");
    }

    private void QuitGame()
    {
        // �Q�[�����I��
        Application.Quit();
    }
}
