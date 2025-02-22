using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeManager : kari
{
    public AudioClip buttonSound;//効果音
    public string nextSceneName;//遷移先のシーン名
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        //効果音再生
        audioSource.PlayOneShot(buttonSound);
        //遷移を遅延
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        //効果音の時間を待つ
        yield return new WaitForSeconds(buttonSound.length);
        //シーン遷移
        SceneManager.LoadScene(nextSceneName);
    }
} 