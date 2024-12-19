using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void OnClick()
    {
        // TitleSceneが既にロードされているかを確認
        Scene titleScene = SceneManager.GetSceneByName("TitleScene");
        if (titleScene.isLoaded)
        {
            // 既にTitleSceneがロードされている場合、何もしない
            return;
        }

        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
    }
}
