using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void OnClick()
    {
        SceneManager.LoadScene("TitleScene");//����NS�V�[���ɔ�Ԃɂ�
    }
}
