using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // スペースキーが押された場合
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 現在のシーンを取得して、次のシーンに移動
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            // 次のシーンが存在すれば、シーン遷移
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("次のシーンは存在しません");
            }
        }
    }
}
