using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Threading;
using Unity.VisualScripting;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goalTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile, eventTile;
    public MasuDB masuDB = new MasuDB(); // MasuDBのインスタンスを追加

    static TilemapManager instance = null;
    public static TilemapManager Instance { get { return instance; } }

    public RouletteController rouletteController = new RouletteController();

    private string scenename;

    public Tilemap Tile;

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
        scenename = "NikuRurettoScene";

        yield return FoodRoulette(scenename, player);
    }

    private IEnumerator HandleVegetableEvent(Player player)
    {
        Debug.Log("野菜イベントが発生！");

        int num = Random.Range(1, 3);
        scenename = "Yasai" + num.ToString() + "RurettoScene";
        
        yield return FoodRoulette(scenename, player);

    }

    private IEnumerator HandleFishEvent(Player player)
    {
        Debug.Log("魚イベントが発生！");
        scenename = "GyokaiRurettoScene";

        yield return FoodRoulette(scenename,player);
    }

    private IEnumerator HandleOtherEvent(Player player)
    {
        Debug.Log("その他イベントが発生！");
        scenename = "SonotaRurettoScene";

        yield return FoodRoulette(scenename,player);

    }

    private IEnumerator HandleLoseEvent(Player player)
    {
        Debug.Log("ハズレイベントが発生！");
        scenename = "HazureRurettoScene";

        yield return FoodRoulette(scenename, player);
    }

    private IEnumerator HandleRandomEvent(Player player)
    {
        Debug.Log("ランダムイベントが発生！");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleBranchEvent(Player player)
    {
        Debug.Log("分岐イベントが発生！");
        yield return new WaitForSeconds(0);
    }

    public IEnumerator FoodRoulette(string scenename,Player player)
    {
        //TilemapRenderer sort = Tile.GetComponent<TilemapManager>();
        var asyncLoad = SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        player.camera.gameObject.SetActive(false);

        rouletteController = GameObject.Find("RouletteController").GetComponent<RouletteController>();
        while (!rouletteController.isFinish)
        {
            yield return null;
        }

        rouletteController.PlayerResult(player);

        yield return new WaitForSeconds(1);

        SceneManager.UnloadSceneAsync(scenename);
        player.camera.gameObject.SetActive(true);
        //Tile.layer = 5;
    }
}