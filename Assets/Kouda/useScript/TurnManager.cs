using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class TurnManager : MonoBehaviour
{
    public FoodRoulette foodRoulette;    // 食材ルーレット管理スクリプト
    public MapManager mapManager;        // マップ管理スクリプト
    public SugorokuGameManager gameManager; // ゲーム全体の管理スクリプト

    private int currentPlayerIndex;

    // プレイヤーのターン開始
    public void StartPlayerTurn(int playerIndex)
    {
        currentPlayerIndex = playerIndex;
        Debug.Log($"プレイヤー {currentPlayerIndex + 1} のターン開始");

        // ルーレット開始
        StartRoulette();
    }

    // ルーレット開始処理
    private void StartRoulette()
    {
        Debug.Log("ルーレットを開始します");
        foodRoulette.StartRoulette(OnRouletteFinished);
    }

    // ルーレット終了時の処理
    private void OnRouletteFinished(int steps)
    {
        Debug.Log($"ルーレット終了: {steps} マス進む");
        mapManager.MovePlayer(currentPlayerIndex, steps, OnMovementComplete);
    }

    // 移動完了時の処理
    private void OnMovementComplete()
    {
        Debug.Log($"プレイヤー {currentPlayerIndex + 1} の移動が完了しました");
        EndTurn();
    }

    // ターン終了処理
    private void EndTurn()
    {
        Debug.Log($"プレイヤー {currentPlayerIndex + 1} のターン終了");
        gameManager.EndTurn();
    }
}
*/