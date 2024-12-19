using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void OnClick()
    {
        // TitleScene�����Ƀ��[�h����Ă��邩���m�F
        Scene titleScene = SceneManager.GetSceneByName("TitleScene");
        if (titleScene.isLoaded)
        {
            // ����TitleScene�����[�h����Ă���ꍇ�A�������Ȃ�
            return;
        }

        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
    }
}
