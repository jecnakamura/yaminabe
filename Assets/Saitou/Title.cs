using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject[] menuButtons; // ボタンオブジェクトをインスペクターで設定
    private int currentIndex = 0;


    private void Start()
    {
        // 初期状態で最初のボタンを選択
        UpdateButtonSelection();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // 十字キーまたはスティックで選択を移動
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
            MoveSelection(-1);
        }

        // Aボタンで決定
        if (Input.GetButtonDown("Submit"))
        {
            menuButtons[currentIndex].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }

    private void MoveSelection(int direction)
    {
        currentIndex = (currentIndex + direction + menuButtons.Length) % menuButtons.Length;
        UpdateButtonSelection();
    }
    private void UpdateButtonSelection()
    {
        // 現在選択中のボタンを強調表示
        EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex]);
    }
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
    //public void OnClickRules()
    //{
    //    SceneManager.LoadScene("RuleScene");
    //}
}
