using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public List<Player> players; // プレイヤー（NPC含む）リスト
    private int currentPlayerIndex = 0;
    //public MapManager mapManager; // マップ管理クラス
    //public GameUIManager uiManager; // UI管理クラス

    private bool isGameFinished = false;

    [SerializeField] GameObject Pl;
    Vector3 spawnPosition;
    void Start()
    {
        spawnPosition = new Vector3(-23, 0, 0);
        for (int i = 0; i < GameData.playerCount; i++)
        {
            Player newPlayer = new Player
            {
                ID = i,
                chara = GameData.selectedCharacters[i],
                ingredients = new List<Ingredient>
                {
                    new Ingredient("", "", 0, 0.0f),
                }
            };
            players.Add(newPlayer);
            Instantiate(Pl, spawnPosition, Quaternion.identity);
        }
        StartCoroutine(TurnCycle());
    }

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

    private IEnumerator HandlePlayerTurn(Player player)
    {
        // UIでターン情報を表示
        //uiManager.ShowTurnInfo(player);

        // ルーレットシーンを開き、結果を取得

        yield return OpenRulletoButton();

        // マスの移動処理
        //mapManager.MovePlayer(player);

        // 止まったマスのイベント処理
        //yield return StartCoroutine(mapManager.HandleTileEvent(player, players));
    }

    private bool CheckAllPlayersFinished()
    {
        return players.TrueForAll(player => player.HasFinished);
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }

    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("ゲーム終了！");
        // 結果発表シーンに移行
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    private IEnumerator OpenRoulette(Player player)
    {
        // ルーレットシーンを開いて結果を受け取る
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);
        Debug.Log("ルーレットオープン");
        int result = RouletteResultHandler.GetResult(); // 仮の結果取得関数
        player.MoveSteps = result;
        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ruretto");
        Debug.Log("ルーレットクローズ"+result);
    }

    public IEnumerator OpenRulletoButton()
    {
        Player player = players[currentPlayerIndex];
        yield return StartCoroutine(OpenRoulette(player));
    }
}
