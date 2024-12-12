using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public enum TileType
{
    Start,  // スタートマス
    Normal, // 通常マス
    Event,  // イベントマス
    Goal    // ゴールマス
}

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Player> players; // プレイヤーリスト
    private Dictionary<Vector3Int, TileType> tileEvents; // タイルイベント辞書
    private Dictionary<Player, Vector3Int> playerPositions; // 各プレイヤーの現在位置
    public Sprite sprite, startTile, goleTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile;



    void Start()
    {
        playerPositions = new Dictionary<Player, Vector3Int>();
        InitializeTileEvents();

        // 各プレイヤーをスタートマスに配置
        foreach (Player player in players)
        {
            Vector3Int startTile = new Vector3Int(0, 0, 0); // スタートマスの位置
            player.transform.position = tilemap.CellToWorld(startTile);
            playerPositions[player] = startTile;
        }

        StartCoroutine(GameLoop());
    }

    private void InitializeTileEvents()
    {
        tileEvents = new Dictionary<Vector3Int, TileType>();
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                if (pos == new Vector3Int(0, 0, 0)) // スタートマス
                {
                    tileEvents[pos] = TileType.Start;
                }
                else if (pos == new Vector3Int(10, 0, 0)) // ゴールマス
                {
                    tileEvents[pos] = TileType.Goal;
                }
                else if (pos.x % 5 == 0) // イベントマス
                {
                    tileEvents[pos] = TileType.Event;
                }
                else
                {
                    tileEvents[pos] = TileType.Normal;
                }
            }
        }
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            foreach (Player player in players)
            {
                if (player.HasFinished) continue; // ゴール済みのプレイヤーはスキップ

                yield return StartCoroutine(HandlePlayerTurn(player));

                if (CheckGameEnd()) yield break;
            }
        }
    }

    private IEnumerator HandlePlayerTurn(Player player)
    {
        Debug.Log($"{player.name} のターン開始");

        // ルーレットシーンで移動距離を取得
        yield return StartCoroutine(OpenRoulette(player));

        // プレイヤーの移動処理
        yield return StartCoroutine(MovePlayer(player));

        // 現在のタイルタイプを取得
        Vector3Int currentPos = playerPositions[player];
        if (tileEvents.TryGetValue(currentPos, out TileType tileType))
        {
            // マスのイベント処理
            yield return StartCoroutine(HandleTileEvent(tileType, player));
        }
    }

    private IEnumerator OpenRoulette(Player player)
    {
        // ルーレットシーンを開いて結果を取得
        yield return SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);

        // 仮の結果取得
        int steps = RouletteResultHandler.GetResult(); // ルーレットの結果を取得する関数（仮定）
        player.MoveSteps = steps;

        yield return SceneManager.UnloadSceneAsync("Ruretto");
    }

    private IEnumerator MovePlayer(Player player)
    {
        int steps = player.MoveSteps;
        Vector3Int startPos = playerPositions[player];

        for (int i = 0; i < steps; i++)
        {
            Vector3Int nextPos = startPos + new Vector3Int(1, 0, 0); // 仮に右方向に移動
            if (tilemap.HasTile(nextPos))
            {
                startPos = nextPos;
                player.transform.position = tilemap.CellToWorld(nextPos);
                yield return new WaitForSeconds(0.3f); // 移動のディレイ
            }
            else
            {
                break;
            }
        }

        playerPositions[player] = startPos; // 最終位置を更新
    }

    private IEnumerator HandleTileEvent(TileType tileType, Player player)
    {
        switch (tileType)
        {
            case TileType.Start:
                Debug.Log("スタートマス: 特にイベントなし");
                break;

            case TileType.Normal:
                Debug.Log("通常マス: 食材追加イベント発生");
                player.AddIngredient(GetRandomIngredient());
                break;

            case TileType.Event:
                Debug.Log("イベントマス: ランダムイベント発生");
                yield return StartCoroutine(HandleRandomEvent(player));
                break;

            case TileType.Goal:
                Debug.Log($"ゴールマス: {player.name} がゴールしました！");
                player.HasFinished = true;
                break;

            default:
                Debug.Log("未知のタイルイベント");
                break;
        }
    }

    private IEnumerator HandleRandomEvent(Player player)
    {
        int randomEvent = Random.Range(0, 3);
        switch (randomEvent)
        {
            case 0:
                Debug.Log("イベント: 食材がランダムで1つ削除されました！");
                player.RemoveRandomIngredient();
                break;
            case 1:
                Debug.Log("イベント: プレイヤーの位置が交換されました！");
                SwapPlayers();
                break;
            case 2:
                Debug.Log("イベント: 他プレイヤーと食材交換！");
                ExchangeIngredients(player, GetRandomOtherPlayer(player));
                break;
        }
        yield return null;
    }

    private Player GetRandomOtherPlayer(Player currentPlayer)
    {
        List<Player> others = players.FindAll(p => p != currentPlayer);
        return others[Random.Range(0, others.Count)];
    }

    private void SwapPlayers()
    {
        // プレイヤーの位置をランダムに交換するロジック
    }

    private void ExchangeIngredients(Player player1, Player player2)
    {
        // プレイヤー同士の食材交換ロジック
    }

    private bool CheckGameEnd()
    {
        if (players.TrueForAll(player => player.HasFinished))
        {
            Debug.Log("全プレイヤーがゴールしました！ゲーム終了！");
            EndGame();
            return true;
        }
        return false;
    }

    private void EndGame()
    {
        Debug.Log("結果画面に移行します。");
        SceneManager.LoadScene("ResultScene");
    }

    private Ingredient GetRandomIngredient()
    {
        // 仮の食材生成
        return new Ingredient("ランダム食材", "種類", Random.Range(1, 10), Random.Range(0.5f, 1.5f));
    }

    public void TileEvent(Player player)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // 取り出した位置情報からタイルマップ用の位置情報(セル座標)を取得
            Vector3Int cellPosition = new Vector3Int(pos.x, pos.y, pos.z);

            // tilemap.HasTile -> タイルが設定(描画)されている座標であるか判定
            if (tilemap.HasTile(cellPosition))
            {
                if (cellPosition == player.CurrentPosition)
                {
                    if (tilemap.GetSprite(cellPosition) == startTile)
                    {
                        Debug.Log("スタートマス");

                        return;
                    }
                    if (tilemap.GetSprite(cellPosition) == goleTile)
                    {
                        Debug.Log("ゴールマス");

                    }
                    if (tilemap.GetSprite(cellPosition) == sprite)
                    {
                        Debug.Log("設定忘れ");
                    }
                    if (tilemap.GetSprite(cellPosition) == nikuTile)
                    {
                        Debug.Log("肉食材");

                    }
                    if (tilemap.GetSprite(cellPosition) == sakanaTile)
                    {
                        Debug.Log("魚食材");

                    }
                    if (tilemap.GetSprite(cellPosition) == yasaiTile)
                    {
                        Debug.Log("野菜食材");

                    }
                    if (tilemap.GetSprite(cellPosition) == hazureTile)
                    {
                        Debug.Log("ハズレ食材");

                    }
                    if (tilemap.GetSprite(cellPosition) == bunnkiTile)
                    {
                        Debug.Log("分岐");

                    }

                }
            }
        }

    }
}
