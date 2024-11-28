using UnityEngine;

public class EventManager : MonoBehaviour
{
    // イベント処理
    public void TriggerEvent(int tileIndex, int playerIndex)
    {
        Debug.Log($"マス {tileIndex} のイベントをプレイヤー {playerIndex + 1} に発生させます");

        // マスの種類に応じたイベントを実行
        switch (GetTileEventType(tileIndex))
        {
            case TileEventType.None:
                Debug.Log("イベントなし");
                break;
            case TileEventType.Bonus:
                ApplyBonus(playerIndex);
                break;
            case TileEventType.Penalty:
                ApplyPenalty(playerIndex);
                break;
            case TileEventType.MiniGame:
                StartMiniGame(playerIndex);
                break;
            default:
                Debug.LogWarning("未定義のイベントタイプ");
                break;
        }
    }

    // マスのイベントタイプを取得
    private TileEventType GetTileEventType(int tileIndex)
    {
        // 実装例: マスごとのイベントタイプを返す（仮）
        return TileEventType.None; // 本来はマスごとのデータから取得
    }

    // ボーナスイベント
    private void ApplyBonus(int playerIndex)
    {
        Debug.Log($"プレイヤー {playerIndex + 1} にボーナスを適用");
        // ボーナス処理を実装（例: スコア加算）
    }

    // ペナルティイベント
    private void ApplyPenalty(int playerIndex)
    {
        Debug.Log($"プレイヤー {playerIndex + 1} にペナルティを適用");
        // ペナルティ処理を実装（例: スコア減算）
    }

    // ミニゲームイベント
    private void StartMiniGame(int playerIndex)
    {
        Debug.Log($"プレイヤー {playerIndex + 1} とミニゲームを開始");
        // ミニゲームシーン遷移などの処理を実装
    }
}

// マスのイベントタイプ列挙型
public enum TileEventType
{
    None,    // イベントなし
    Bonus,   // ボーナス
    Penalty, // ペナルティ
    MiniGame // ミニゲーム
}
