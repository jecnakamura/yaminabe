using UnityEngine;

public enum MassEventType
{
    None,           // イベントなし
    AddIngredient,  // ランダムな食材の追加
    RemoveIngredient, // ランダムな食材の削除
    SwapPositions,  // プレイヤーたちの場所を入れ替える
    GrantKey,       // 鍵の付与
    MiniGame,       // ミニゲーム
    Goal            // ゴール
}

public class Mass : MonoBehaviour
{
    public MassEventType eventType;  // このマスのイベントの種類
}
