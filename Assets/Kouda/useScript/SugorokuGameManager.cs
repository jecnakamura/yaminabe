using UnityEngine;

public class SugorokuGameManager : MonoBehaviour
{
    private int currentPlayerIndex = 0; // 現在のプレイヤーのインデックス
    private int totalPlayers;          // 総プレイヤー数

    public TurnManager turnManager;    // ターン管理スクリプト
    public MapManager mapManager;      // マップ管理スクリプト
    public ScoreManager scoreManager;  // スコア管理スクリプト

    private void Start()
    {
        InitializeGame();
    }

    // ゲームの初期化
    private void InitializeGame()
    {
        totalPlayers = GameData.playerCount;
        if (totalPlayers == 0)
        {
            Debug.LogError("プレイヤー人数が設定されていません！");
            return;
        }

        Debug.Log($"ゲームを開始します。プレイヤー人数: {totalPlayers}");
        StartTurn();
    }

    // ターン開始
    private void StartTurn()
    {
        Debug.Log($"プレイヤー {currentPlayerIndex + 1} のターン開始");
        //turnManager.StartPlayerTurn(currentPlayerIndex);
    }

    // ターン終了時の処理
    public void EndTurn()
    {
        Debug.Log($"プレイヤー {currentPlayerIndex + 1} のターン終了");
        currentPlayerIndex = (currentPlayerIndex + 1) % totalPlayers;

        // ゲーム終了条件の確認
        if (CheckGameEnd())
        {
            EndGame();
        }
        else
        {
            StartTurn();
        }
    }

    // ゲーム終了条件の確認
    private bool CheckGameEnd()
    {
        // 全プレイヤーがゴールした場合など、終了条件をここに設定
        return false;
    }

    // ゲーム終了時の処理
    private void EndGame()
    {
        Debug.Log("ゲームが終了しました。結果画面へ移行します。");
        // スコア計算処理を呼び出す
        //scoreManager.CalculateFinalScores();

        // 結果画面への遷移
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
}
