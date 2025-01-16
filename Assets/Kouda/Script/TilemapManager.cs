using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goalTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile, eventTile;
    public MasuDB masuDB = new MasuDB(); // MasuDBのインスタンスを追加

    static TilemapManager instance = null;
    public static TilemapManager Instance { get { return instance; } }

    public RouletteController rouletteController = new RouletteController();

    void Start()
    {
        // 必要な初期化処理をここに追加
        instance = this;
    }

    public IEnumerator TileEvent(Player player)
    {

        // プレイヤーの位置をセル座標に変換
        Vector3Int playerCell = tilemap.WorldToCell(player.transform.position);
        TileBase currentTile = tilemap.GetTile(playerCell);

        if (currentTile != null)
        {
            Debug.Log("現在のタイル: " + currentTile.name);

            // タイルに応じた処理
            if (currentTile == startTile)
            {
                Debug.Log("スタートマス");
            }
            else if (currentTile == goalTile)
            {
                Debug.Log("ゴールマス");
            }
            else if (currentTile == nikuTile)
            {
                Debug.Log("肉食材マス");
                //NIKU.GetNIKU(player.Ingredient);
                yield return StartCoroutine(HandleMeatEvent(player));
            }
            else if (currentTile == sakanaTile)
            {
                Debug.Log("魚食材マス");
                yield return StartCoroutine(HandleFishEvent(player));
            }
            else if (currentTile == yasaiTile)
            {
                Debug.Log("野菜食材マス");
                yield return StartCoroutine(HandleVegetableEvent(player));
            }
            else if (currentTile == hazureTile)
            {
                Debug.Log("ハズレ食材マス");
                yield return StartCoroutine(HandleLoseEvent(player));
            }
            else if (currentTile == bunnkiTile)
            {
                Debug.Log("分岐マス");
                yield return StartCoroutine(HandleBranchEvent(player));
            }
            else if (currentTile == eventTile)
            {
                Debug.Log("イベントマス");
                yield return StartCoroutine(HandleOtherEvent(player));
            }
            else
            {
                Debug.Log("不明なタイル");
            }
        }
        else
        {
            Debug.Log("タイルが見つかりませんでした。");
        }

        // プレイヤーの現在位置に対応するマス情報を取得
        MasuData masu = masuDB.GetMasuData(player.nowIndex); // プレイヤーの位置に対応するMasuDataを取得
        if (masu != null)
        {
            Debug.Log($"マス {masu.index} のイベントが発動");

            // マスに関連するイベントタイプを確認
            switch (masu.ev)
            {
                case EventType.Meat:
                    yield return StartCoroutine(HandleMeatEvent(player));
                    break;

                case EventType.Vegetable: 
                    yield return StartCoroutine(HandleVegetableEvent(player));
                    break;

                case EventType.Fish:
                    yield return StartCoroutine(HandleFishEvent(player));
                    break;

                case EventType.Other:
                    yield return StartCoroutine(HandleOtherEvent(player));
                    break;

                case EventType.Lose:
                    yield return StartCoroutine(HandleLoseEvent(player));
                    break;

                case EventType.RandomExchange:
                    yield return StartCoroutine(HandleRandomEvent(player));
                    break;

                case EventType.Branch:
                    yield return StartCoroutine(HandleBranchEvent(player));
                    break;

                case EventType.Start:
                    Debug.Log("スタートマス");
                    break;

                case EventType.Goal:
                    Debug.Log("ゴールマス");
                    break;

                default:
                    Debug.LogWarning($"未定義のイベントタイプ {masu.ev} が発生しました");
                    break;
            }
        }
        else
        {
            Debug.LogWarning("プレイヤーの位置に対応するマスデータが見つかりませんでした");
        }
    }

    // 各イベントの処理
    private IEnumerator HandleMeatEvent(Player player)
    {
        Debug.Log("肉イベントが発生！");
        // 肉に関連する処理をここに記述
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleVegetableEvent(Player player)
    {
        Debug.Log("野菜イベントが発生！");

        int num = Random.Range(1, 3);
        string scenename = "Yasai" + num.ToString() + "RurettoScene";
        
        yield return SceneManager.LoadSceneAsync(scenename);
        player.camera.gameObject.SetActive(false);

        rouletteController.PlayerResult(player);

        yield return new WaitForSeconds(1);

        yield return SceneManager.UnloadSceneAsync(scenename);
        player.camera.gameObject.SetActive(true);


    }

    private IEnumerator HandleFishEvent(Player player)
    {
        Debug.Log("魚イベントが発生！");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleOtherEvent(Player player)
    {
        Debug.Log("その他イベントが発生！");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleLoseEvent(Player player)
    {
        Debug.Log("ハズレイベントが発生！");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleRandomEvent(Player player)
    {
        Debug.Log("ランダムイベントが発生！");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleBranchEvent(Player player)
    {
        Debug.Log("分岐イベントが発生！");
        yield return new WaitForSeconds(1);
    }
}