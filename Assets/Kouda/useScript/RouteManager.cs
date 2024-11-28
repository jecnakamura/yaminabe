using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Vector3[] routes; // マップ上のルート情報（各マスの位置を格納）

    public Vector3 GetTilePosition(int tileIndex)
    {
        if (tileIndex >= 0 && tileIndex < routes.Length)
        {
            return routes[tileIndex];
        }
        else
        {
            Debug.LogWarning($"無効なマス番号 {tileIndex}");
            return Vector3.zero;
        }
    }

    public int[] GetPossibleRoutes(int currentTileIndex)
    {
        Debug.Log($"現在位置 {currentTileIndex} から選択可能なルートを取得");
        // 分岐点で選択可能なマス番号を返す（仮に2つの選択肢を固定）
        return new int[] { currentTileIndex + 1, currentTileIndex + 2 };
    }

    public bool IsKeyRoute(int tileIndex)
    {
        // 特定のマスが鍵ルートであるか判定（仮）
        return tileIndex % 10 == 0; // 仮に10マスごとを鍵ルートとする
    }
}
