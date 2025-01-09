using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeManager : kari
{
    public AudioClip buttonSound;//���ʉ�
    public string nextSceneName;//�J�ڐ�̃V�[����
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        //���ʉ��Đ�
        audioSource.PlayOneShot(buttonSound);
        //�J�ڂ�x��
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        //���ʉ��̎��Ԃ�҂�
        yield return new WaitForSeconds(buttonSound.length);
        //�V�[���J��
        SceneManager.LoadScene(nextSceneName);
    }
} 