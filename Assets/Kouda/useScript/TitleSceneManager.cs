using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
public class TitleSceneManager : MonoBehaviour
{
    private int selectedPlayerCount = 0;     // 選択されたプレイヤー人数
    private int maxPlayers = 4;              // 最大プレイヤー数（固定）

    public void Start()
    {
        GameData.playerCount = 0;
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

    // ルール説明画面へ遷移
    public void GoToRules()
    {
        SceneManager.LoadScene("RuleScene");  // ルール説明シーンへ遷移
    }
}
*/