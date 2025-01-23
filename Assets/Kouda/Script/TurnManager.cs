using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum TurnState
    {
        CommandSelect,  //0
        ViewMap,        //1
        LookList,       //2
        Roulette,       //3
        PlayerMove,     //4
        Event,          //5
        End,            //6
        Bonus,          //7
    }

    public enum RouletteNameFile
    {
        Gyokai,
        Niku,
        Syusyoku,
        Yasai1,
        Yasai2,
        Yasai3,
        Sonota,
        Hazure,
        
    }

    public TurnState state = TurnState.CommandSelect;
    public Character availableCharacter;    // 全キャラクターリスト

    public List<Player> players; // プレイヤー（NPC含む）リスト
    private int currentPlayerIndex = 0;
    public MapManager mapManager; // マップ管理クラス
    public CameraController cameraController; // カメラ管理クラス
    public UIManager uiManager; // UI管理クラス
    public TilemapManager tilemapManager;
    public List<GameObject> commandButtons;
    public MasuDB masuDB;
    public Button RoulettteGameButton;
    private bool isGameFinished = false;
    private bool isStateEnd = false;

    [SerializeField] GameObject Pl;
    Vector3 spawnPosition;

    void Start()
    {
        InitializePlayers();
        StartCoroutine(TurnCycle());
    }

    private void Update()
    {
        //Debug.Log(state.ToString());
        if (state == TurnState.CommandSelect)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Player currentPlayer = players[currentPlayerIndex];
                NextState(TurnState.Roulette);
                StartCoroutine(HandleState(currentPlayer));
            }
        }
    }

    private void InitializePlayers()
    {
        Vector3 scale = new Vector3(0.25f, 0.25f, 1.0f);
        spawnPosition = new Vector3(-23, 3, 0);

        if (GameData.selectedCharacters[0] == null)
        {
            Character selectedCharacter = availableCharacter;

            GameData.selectedCharacters[0] = selectedCharacter;
        }
        for (int i = 0; i < GameData.playerCount; i++)
        {
            var obj = Instantiate(Pl, spawnPosition, Quaternion.identity);
            var player = obj.GetComponent<Player>();
            obj.transform.localScale = scale;

            player.ID = i;
            player.chara = GameData.selectedCharacters[i];
            player.ingredients = new List<Ingredient>();

            players.Add(player);
            player.SetCharaImage();
        }
    }

    private IEnumerator TurnCycle()
    {
        Player currentPlayer = players[currentPlayerIndex];
        ActivateCamera(currentPlayer);
        uiManager.ShowTurnInfo(currentPlayer);
        // ここで全プレイヤーのHasFinishedを確認
        if (AllPlayersFinished())
        {
            EndGame();
            yield break; // ゲーム終了後はターンループを終了
        }

        while (!isGameFinished)
        {
            yield return StartCoroutine(HandleState(currentPlayer));
        }

        EndGame();
    }
    private bool AllPlayersFinished()
    {
        // 全プレイヤーがHasFinished == trueかどうかをチェック
        foreach (var player in players)
        {
            if (!player.HasFinished)
            {
                return false;
            }
        }
        return true;
    }
    private void ActivateCamera(Player player)
    {
        foreach (var p in players)
        {
            p.camera.gameObject.SetActive(false);
        }
        player.camera.gameObject.SetActive(true);
    }

    private IEnumerator HandleState(Player currentPlayer)
    {
        switch (state)
        {
            case TurnState.CommandSelect:
                RoulettteGameButton.gameObject.SetActive(true);
                yield return StartCoroutine(HandleCommandSelect(currentPlayer));
                break;

            case TurnState.ViewMap:
                yield return StartCoroutine(HandleViewMap(currentPlayer));
                break;

            case TurnState.LookList:
                yield return StartCoroutine(HandleLookList());
                break;

            case TurnState.Roulette:
                Debug.Log("Rurettoシーンが読み込まれました");
                yield return StartCoroutine(HandleRoulette(currentPlayer));
                break;

            case TurnState.PlayerMove:
                yield return StartCoroutine(HandlePlayerMove(currentPlayer));
                break;

            case TurnState.Event:
                yield return StartCoroutine(HandleEvent(currentPlayer));
                break;

            case TurnState.End:
                isGameFinished = true;
                break;

            case TurnState.Bonus:
                yield return StartCoroutine(HandleGoalBouns(currentPlayer));
                break;
        }
    }

    private IEnumerator HandleCommandSelect(Player player)
    {
        if (player.HasFinished == false)
        {
            ToggleCommandButtons(true);
            commandButtons[2].SetActive(false);

        }
        else
        {
            commandButtons[0].SetActive(false) ;
            commandButtons[1].SetActive(false) ;
            commandButtons[2].SetActive(true);
        }



        while (!isStateEnd)
        {
            HandleControllerInputForCommand();
            yield return null;
        }

        ToggleCommandButtons(false);
        NextState(state);
    }

    public void OnButtonClick(int type)
    {
        Player currentPlayer = players[currentPlayerIndex];
        NextState((TurnState)type);
        StartCoroutine(HandleState(currentPlayer));
    }

    private void HandleControllerInputForCommand()
    {
        if (Input.GetButtonDown("Submit"))
        {
            // 決定ボタンが押された場合の処理
            isStateEnd = true;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            // キャンセルボタンで状態を戻す
            state = TurnState.CommandSelect;
            isStateEnd = true;
        }
    }

    private IEnumerator HandleViewMap(Player player)
    {
        player.camera.gameObject.SetActive(false);
        cameraController.gameObject.SetActive(true);

        while (!isStateEnd)
        {
            HandleControllerInputForViewMap();
            yield return null;
        }

        NextState(TurnState.CommandSelect);
    }

    private void HandleControllerInputForViewMap()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isStateEnd = true;
        }
    }

    private IEnumerator HandleLookList()
    {
        while (!isStateEnd)
        {
            HandleControllerInputForLookList();
            yield return null;
        }

        NextState(TurnState.CommandSelect);
    }

    private void HandleControllerInputForLookList()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isStateEnd = true;
        }
    }

    private IEnumerator HandleRoulette(Player player)
    {
        RoulettteGameButton.gameObject.SetActive(false);

        RouletteResultHandler.SetEnd(false);

        yield return SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);

        ToggleCommandButtons(false);
        player.camera.gameObject.SetActive(false);

        while (!RouletteResultHandler.IsEnd())
        {
            HandleControllerInputForRoulette();
            yield return null;
        }

        int result = RouletteResultHandler.GetResult();
        player.MoveSteps = result;
        yield return new WaitForSeconds(2);

        yield return SceneManager.UnloadSceneAsync("Ruretto");
        player.camera.gameObject.SetActive(true);

        NextState(player.MoveSteps == 0 ? TurnState.CommandSelect : TurnState.PlayerMove);

        StartCoroutine(HandleState(player));
    }

    private void HandleControllerInputForRoulette()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            RouletteResultHandler.SetEnd(true);
        }
    }

    private IEnumerator HandlePlayerMove(Player player)
    {
        for (int i = 0; i < player.MoveSteps; i++)
        {
            Vector3 targetPos = player.transform.position + new Vector3(3.5f, 0.0f, 0.0f);
            yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
            if (tilemapManager.masuDB.data[player.nowIndex].ev == EventType.Branch)
            {
                yield return StartCoroutine(BranchEvent(player));
            }
            else if (tilemapManager.masuDB.data[player.nowIndex].ev == EventType.Goal)
            {
                player.HasFinished = true;
                break;
            }
        }

        yield return StartCoroutine(tilemapManager.TileEvent(player));
        player.MoveSteps = 0;


        NextState(TurnState.Event);
        NextPlayer();
        StartCoroutine(TurnCycle());
    }

    private IEnumerator HandleEvent(Player player)
    {
        tilemapManager.TileEvent(player);
        NextPlayer();
        yield return null;
    }

    private void ToggleCommandButtons(bool isActive)
    {
        foreach (var btn in commandButtons)
        {
            btn.SetActive(isActive);
        }
    }

    private void NextState(TurnState newState)
    {
        state = newState;
        isStateEnd = false;
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        NextState(TurnState.CommandSelect);
    }

    private void EndGame()
    {
        Debug.Log("ゲーム終了！");

        // GameManager にプレイヤーデータをセット
        GameManager.Instance.SetPlayers(players);

        // 結果シーンに遷移
        SceneManager.LoadScene("ResultScene");
    }

    private IEnumerator BranchEvent(Player player)
    {
        // 分岐先マスを取得
        MasuData currentMasu = masuDB.GetMasuData(player.nowIndex);
        List<int> nextIndices = currentMasu.next;

        if (nextIndices == null || nextIndices.Count == 0)
        {
            Debug.LogWarning("分岐先がありません。");
            yield break;
        }
        if (nextIndices.Count == 1)
        {
            //上から真ん中へ
            if (player.nowIndex == 13 || player.nowIndex == 50 || player.nowIndex == 81)
            {
                Vector3 targetPos = player.transform.position + new Vector3(3.5f, -4.0f, 0.0f);
                yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
            }
            //下から真ん中へ
            else if (player.nowIndex == 35 || player.nowIndex == 68 || player.nowIndex == 97)
            {
                Vector3 targetPos = player.transform.position + new Vector3(3.5f, 4.0f, 0.0f);
                yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
            }
            //真ん中のまま
            else
            {
                Vector3 targetPos = player.transform.position + new Vector3(3.5f, 0.0f, 0.0f);
                yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
            }
            yield break;
        }
        else
        {
            // 分岐選択UIを表示
            uiManager.ShowBranchOptions(nextIndices, player);

            // プレイヤーが選択するまで待機
            int selectedIndex = -1;
            bool isOptionSelected = false;

            // イベントリスナーを追加
            uiManager.OnBranchSelected += (index) =>
            {
                selectedIndex = index;
                isOptionSelected = true;
            };

            // 選択が完了するまで待機
            while (!isOptionSelected)
            {
                // この部分でプレイヤーの移動処理を待機
                yield return null;
            }

            // イベントリスナーを解除
            uiManager.OnBranchSelected -= (index) =>
            {
                selectedIndex = index;
                isOptionSelected = true;
            };

            // UIを非表示にする
            uiManager.HideBranchOptions();
            player.nowIndex = selectedIndex;

            Debug.Log($"プレイヤー {player.ID} が分岐を選択: マス {selectedIndex}");

            // 選択された分岐先に移動
            //上の選択肢
            if (selectedIndex == 3 || selectedIndex == 42 || selectedIndex == 74)
            {
                Vector3 targetPos = player.transform.position + new Vector3(3.5f, 4.0f, 0.0f);
                yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
            }
            //下の選択肢
            else if (selectedIndex == 25 || selectedIndex == 60 || selectedIndex == 90)
            {
                Vector3 targetPos = player.transform.position + new Vector3(3.5f, -4.0f, 0.0f);
                yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));

            }
            //真ん中の選択肢
            else
            {
                Vector3 targetPos = player.transform.position + new Vector3(3.5f, 0.0f, 0.0f);
                yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
            }
            player.nowIndex -= 1;

        }
    }

    private IEnumerator HandleGoalBouns(Player player)
    {
        int sceneNum = UnityEngine.Random.Range(0, 6);

        string scenename = Enum.GetName(typeof(RouletteNameFile), sceneNum)+"RurettoScene";
        ToggleCommandButtons(false);
        yield return StartCoroutine(tilemapManager.FoodRoulette(scenename, player));
        NextState(TurnState.CommandSelect);
        NextPlayer();
        yield return StartCoroutine(TurnCycle());
    }
}
