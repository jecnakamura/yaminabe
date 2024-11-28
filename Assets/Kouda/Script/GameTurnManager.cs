using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public class GameTurnManager : MonoBehaviour
{
    public List<Player> players;  // プレイヤーリスト
    public MapManager mapManager;  // マップ管理
    public UIManager uiManager;    // UI管理
    private int currentPlayerIndex = 0;
    private bool isGameFinished = false;

    void Start()
    {
        StartCoroutine(TurnCycle());
    }

    // ターン順に処理を実行
    private IEnumerator TurnCycle()
    {
        while (!isGameFinished)
        {
            Player currentPlayer = players[currentPlayerIndex];
            yield return StartCoroutine(HandlePlayerTurn(currentPlayer));

            if (CheckAllPlayersFinished())
            {
                EndGame();
                yield break;
            }

            NextPlayer();
        }
    }

    // プレイヤーのターンを処理
    private IEnumerator HandlePlayerTurn(Player player)
    {
        // UIでターン情報を表示
        uiManager.ShowTurnInfo(player);

        // ルーレットを回す
        /*if (!player.IsNPC)
        {
            yield return StartCoroutine(player.RollRoulette());
        }
        else
        {
            player.MoveSteps = Random.Range(1, 7);  // NPCはランダムにステップ進む
        }
        */
        // マスの移動処理
        mapManager.MovePlayer(player);

        // イベント処理
        yield return StartCoroutine(mapManager.HandleTileEvent(player));
    }

    // 全員がゴールしたか確認
    private bool CheckAllPlayersFinished()
    {
        return players.TrueForAll(player => player.HasFinished);
    }

    // 次のプレイヤーのターンに移る
    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }

    // ゲーム終了処理
    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("ゲーム終了！");
        // 結果発表シーンに移行
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
}