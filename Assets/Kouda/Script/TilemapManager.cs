using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public enum TileType
{
    Start,  // スタートマス
    Normal, // 通常マス
    Event,  // イベントマス
    Goal    // ゴールマス
}

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goleTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile;



    void Start()
    {

    }

    public IEnumerator TileEvent(Player player)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // 取り出した位置情報からタイルマップ用の位置情報(セル座標)を取得
            Vector3Int cellPosition = new Vector3Int(pos.x, pos.y, pos.z);

            // tilemap.HasTile -> タイルが設定(描画)されている座標であるか判定
            if (tilemap.HasTile(cellPosition))
            {
                if (cellPosition == player.CurrentPosition)
                {
                    if (tilemap.GetSprite(cellPosition) == startTile)
                    {
                        Debug.Log("スタートマス");

                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == goleTile)
                    {
                        Debug.Log("ゴールマス");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == sprite)
                    {
                        Debug.Log("設定忘れ");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == nikuTile)
                    {
                        Debug.Log("肉食材");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == sakanaTile)
                    {
                        Debug.Log("魚食材");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == yasaiTile)
                    {
                        Debug.Log("野菜食材");
                        yield return null;

                    }
                    if (tilemap.GetSprite(cellPosition) == hazureTile)
                    {
                        Debug.Log("ハズレ食材");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == bunnkiTile)
                    {
                        Debug.Log("分岐");
                        yield return null;
                    }

                }
                else
                {
                    Debug.Log("どこ～？");
                    yield return null;
                }
            }
        }

    }
}
