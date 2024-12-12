using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum TurnState
    {
        CommandSelect,
        ViewMap,
        LookList,
        Roulette,
        PlayerMove,
        Event,
        End,
    }
    public TurnState state = TurnState.CommandSelect;

    public List<Player> players; // プレイヤー（NPC含む）リスト
    private int currentPlayerIndex = 0;
    public MapManager mapManager; // マップ管理クラス
    public CameraController cameraController;   //カメラ管理クラス
    public UIManager uiManager; // UI管理クラス

    public List<GameObject> commandButtons;


    private bool isGameFinished = false;

    bool isStateEnd = false;

    [SerializeField] GameObject Pl;
    Vector3 spawnPosition;


    void Start()
    {
        cameraController = GetComponent<CameraController>();
        Vector3 scale = new Vector3(0.25f, 0.25f, 1.0f);
        spawnPosition = new Vector3(-23, 3, 0);
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

        StartCoroutine(TurnCycle());
    }

   

    private IEnumerator TurnCycle()
    {
        Player currentPlayer = players[currentPlayerIndex];
        // UIでターン情報を表示
        uiManager.ShowTurnInfo(currentPlayer);
        switch (state)
        {
            case TurnState.CommandSelect:
                {
                    //cameraController.FollowPlayer(currentPlayer);
                    yield return StartCoroutine(HandleCommandSelect(currentPlayer));
                }
                break;

            case TurnState.ViewMap:
                {
                }
                break;

            case TurnState.LookList:
                {
                }
                break;

            case TurnState.Roulette:
                {
                    StartCoroutine(HandleRoulette(currentPlayer));
                }
                break;

            case TurnState.PlayerMove:
                {
                    yield return StartCoroutine(HandlePlayerTurn(currentPlayer));
                }
                break;

            case TurnState.Event:
                {
                    if (CheckAllPlayersFinished())
                    {
                        state = TurnState.End;
                        StartCoroutine(TurnCycle());
                    }
                    
                    NextPlayer();
                }
                break;

            case TurnState.End:
                EndGame();
                yield break;
        }
    }


    public void OnCommandButton(int type)
    {
        isStateEnd = true;
        state = (TurnState)type;
    }

    IEnumerator HandleCommandSelect(Player player)
    {
        foreach(var btn in commandButtons)
        {
            btn.SetActive(true);
        }
        while (!isStateEnd) yield return null;

        foreach (var btn in commandButtons)
        {
            btn.SetActive(false);
        }
        NextState(state);
    }

    private IEnumerator HandleRoulette(Player player)
    {
        RouletteResultHandler.SetEnd(false);

        // ルーレットシーンを開いて結果を受け取る
        yield return SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);
        Debug.Log("ルーレットオープン");

        // 終了待ち
        while (!RouletteResultHandler.IsEnd())
        {
            yield return null;
        }

        int result = RouletteResultHandler.GetResult(); // 仮の結果取得関数
        player.MoveSteps = result;
        
        yield return new WaitForSeconds(1);
        yield return SceneManager.UnloadSceneAsync("Ruretto");
        Debug.Log("ルーレットクローズ" + result);
        if (player.MoveSteps == 0)//ルーレットを回さずに閉じた場合
        {
            NextState(TurnState.CommandSelect);
            yield break;
        }

        NextState(TurnState.PlayerMove);
    }

    void NextState(TurnState st)
    {
        state = st;
        isStateEnd = false;
        StartCoroutine(TurnCycle());
    }

    private IEnumerator HandlePlayerTurn(Player player)
    {

        // マスの移動処理
        for (int i = 0; i < player.MoveSteps; i++)
        {
            Vector3 targetPos = player.transform.position + new Vector3(3.5f, 0.0f, 0.0f);
            yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
        }

        // 止まったマスのイベント処理
        //yield return StartCoroutine(mapManager.HandleTileEvent(player, players));

        
        yield return null;
        player.MoveSteps = 0;
        NextState(TurnState.Event);
    }

    private bool CheckAllPlayersFinished()
    {
        return players.TrueForAll(player => player.HasFinished);
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        NextState(TurnState.CommandSelect);
    }

    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("ゲーム終了！");
        // 結果発表シーンに移行
        SceneManager.LoadScene("ResultScene");
    }

    
}
