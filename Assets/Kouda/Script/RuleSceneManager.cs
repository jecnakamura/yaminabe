using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // ルール説明画面へ遷移
    public void GoToTitle()
    {
        SceneManager.LoadScene("TiteScens");
    }

}
