using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeManager : kari
{
    public AudioClip buttonSound;//Œø‰Ê‰¹
    public string nextSceneName;//‘JˆÚæ‚ÌƒV[ƒ“–¼
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        //Œø‰Ê‰¹Ä¶
        audioSource.PlayOneShot(buttonSound);
        //‘JˆÚ‚ğ’x‰„
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        //Œø‰Ê‰¹‚ÌŠÔ‚ğ‘Ò‚Â
        yield return new WaitForSeconds(buttonSound.length);
        //ƒV[ƒ“‘JˆÚ
        SceneManager.LoadScene(nextSceneName);
    }
} 