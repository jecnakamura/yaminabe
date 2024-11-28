using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform[] mapTiles; // マップ上のタイル（位置）

    // プレイヤーを特定のマスに移動させる
    public void MovePlayer(int playerIndex, int steps, System.Action onComplete)
    {
        Debug.Log($"プレイヤー {playerIndex + 1} を {steps} マス移動させます");
        // プレイヤーの移動ロジックをここに実装（例: アニメーション、イベント処理など）

        // 移動完了後、コールバックを呼び出し
        onComplete?.Invoke();
    }

    // 特定のマスに応じたイベントを実行
    public void TriggerTileEvent(int tileIndex, int playerIndex)
    {
        Debug.Log($"マス {tileIndex} でプレイヤー {playerIndex + 1} のイベントを実行");
        // イベント処理をここに実装
    }
}
