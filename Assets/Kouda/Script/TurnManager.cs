using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // プレイヤーのリスト（NPC含む）
    public List<Player> players;  // 各プレイヤーのオブジェクト（仮データとしてGameObjectリストを想定）
    private int currentPlayerIndex = 0;  // 現在のプレイヤーのインデックス
    private bool isTurnActive = false;   // ターンが進行中かどうかを管理するフラグ

    [SerializeField] private GameObject Pl;
    public Vector3 spawnPosition; // 出現させたい位置
    void Start()
    {
        spawnPosition = new Vector3(-23,0,0);
        for (int i = 0; i < GameData.playerCount; i++)
        {
            Instantiate(Pl,spawnPosition,Quaternion.identity);
            Player newPlayer = new Player
            {
                ID = i,
                chara = GameData.selectedCharacters[i],
                ingredients = new List<Ingredient>
                {
                    new Ingredient("", "", 0, 0.0f), // Ingredientをリストに追加
                }
            };
            players.Add(newPlayer);
        }
        StartCoroutine(TurnCycle());  // ターンのループを開始
    }

    // ターンのサイクルを管理するコルーチン
    private IEnumerator TurnCycle()
    {
        while (true)
        {
            yield return StartCoroutine(PlayerTurn(players[currentPlayerIndex])); // 現在のプレイヤーのターンを開始
            EndTurn(); // ターン終了処理
            yield return new WaitForSeconds(1f); // ターン間のインターバル
        }
    }

    // 各プレイヤーのターン処理
    private IEnumerator PlayerTurn(Player player)
    {
        isTurnActive = true;
        Debug.Log($"Player {currentPlayerIndex + 1}'s Turn");

        // ここでプレイヤーのターンの処理を実行する（ダイスを振る、移動など）
        int MoveCnt = Random.Range(1, 8);

        // 仮実装として一定時間待機
        yield return new WaitForSeconds(2f);  // ターンの仮処理として2秒待機

        isTurnActive = false;
    }

    // ターン終了処理
    private void EndTurn()
    {
        // 現在のプレイヤーのターンが終了したので次のプレイヤーに移動
        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;  // 全プレイヤーがターンを終えたら最初に戻る
        }
    }
}
