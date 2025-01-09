using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    private int selectedPlayerCount = 0;     // 選択されたプレイヤー人数
    private int maxPlayers = 4;              // 最大プレイヤー数（固定）
    public GameObject[] playerCountButtons; // 1～4人選択のボタン
    public GameObject backButton; // 戻るボタン
    private int currentIndex = 0;
    public void Start()
    {
        GameData.playerCount = 0;
        UpdateButtonSelection();
    }
    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        // 十字キーまたはスティックで選択を移動
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
            MoveSelection(-1);
        }

        // Aボタンで決定
        if (Input.GetButtonDown("Submit"))
        {
            if (currentIndex < playerCountButtons.Length)
            {
                playerCountButtons[currentIndex].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            else
            {
                backButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }

        // Bボタンでタイトル画面に戻る
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    private void MoveSelection(int direction)
    {
        int buttonCount = playerCountButtons.Length + 1; // 戻るボタンを含む
        currentIndex = (currentIndex + direction + buttonCount) % buttonCount;
        UpdateButtonSelection();
    }

    private void UpdateButtonSelection()
    {
        if (currentIndex < playerCountButtons.Length)
        {
            EventSystem.current.SetSelectedGameObject(playerCountButtons[currentIndex]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(backButton);
        }
    }
    // プレイヤー人数をボタンで選択するメソッド
    public void SelectPlayerCount(int playerCount)
    {
        if (playerCount > 0 && playerCount <= maxPlayers)
        {
            selectedPlayerCount = playerCount;
            Debug.Log($"選択されたプレイヤー人数: {selectedPlayerCount}");
            StartGame();
        }
        else
        {
            Debug.LogWarning("無効なプレイヤー人数が選択されました");
        }
    }

    // ゲーム開始ボタン
    public void StartGame()
    {
        if (selectedPlayerCount > 0)
        {
            GameData.playerCount = selectedPlayerCount;  // 選択された人数を保存
            SceneManager.LoadScene("CharacterSelectionScene");  // キャラクター選択シーンへ遷移
        }
        else
        {
            Debug.LogWarning("プレイヤー人数を選択してください");
        }
    }

    public void GoToRules()
    {
        SceneManager.UnloadSceneAsync("TitleScene");
    }
}
