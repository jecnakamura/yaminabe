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
    // ���[��������ʂ֑J��
    public void GoToTitle()
    {
        SceneManager.LoadScene("TiteScens");
    }

}
