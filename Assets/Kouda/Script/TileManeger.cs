using UnityEngine;

public class TileManeger : MonoBehaviour
{
    public Vector3 RayGoal;

    public void GetTile(Player player)
    {
        var goal = player.camera.WorldToScreenPoint(RayGoal);
        Ray ray = player.camera.ScreenPointToRay(goal);
        int layerMask = 0xffff;
        var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 100, layerMask);

        if (hit.collider != null)
        {
            // 衝突したオブジェクトの色を取得
            var hitObject = hit.collider.gameObject;
            Renderer renderer = hitObject.GetComponent<Renderer>();

            if (renderer != null)
            {
                // タイルの色を取得（レンダラーが存在する場合）
                Color tileColor = renderer.material.color;
                Debug.Log("Hit object color: " + tileColor);

                // ここでタイルの色に基づいて処理を実行できます
                if (tileColor == Color.red)
                {
                    Debug.Log("赤いタイルに当たりました");
                }
                else if (tileColor == Color.blue)
                {
                    Debug.Log("青いタイルに当たりました");
                }
                // その他の色の場合の処理も追加可能
            }
        }
    }

}
