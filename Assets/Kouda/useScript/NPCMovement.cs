using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public int currentTileIndex = 0; // 現在のマス位置
    public int npcIndex;            // NPCの番号

    public void Move(int steps)
    {
        Debug.Log($"NPC {npcIndex + 1} が {steps} ステップ進みます");

        for (int i = 0; i < steps; i++)
        {
            currentTileIndex++;
            Debug.Log($"NPC {npcIndex + 1} はマス {currentTileIndex} に到達");
            // マスのイベントを処理
            HandleTileEvent(currentTileIndex);

            // ゴール判定（例: マス数が規定値を超えた場合）
            if (IsGoalReached(currentTileIndex))
            {
                Debug.Log($"NPC {npcIndex + 1} がゴールに到達しました");
                break;
            }
        }
    }

    private void HandleTileEvent(int tileIndex)
    {
        Debug.Log($"マス {tileIndex} のイベントを処理中");
        // EventManager でイベントをトリガー
        FindObjectOfType<EventManager>().TriggerEvent(tileIndex, npcIndex);
    }

    private bool IsGoalReached(int tileIndex)
    {
        // ゴール条件を判定（仮に100マスでゴールとする）
        return tileIndex >= 100;
    }
}
