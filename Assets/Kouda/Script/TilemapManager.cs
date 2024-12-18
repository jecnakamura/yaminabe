using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goleTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile, eventTile;



    void Start()
    {

    }

    public IEnumerator TileEvent(Player player){
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // 取り出した位置情報からタイルマップ用の位置情報(セル座標)を取得
            Vector3Int cellPosition = new Vector3Int(pos.x, pos.y, pos.z);

            // tilemap.HasTile -> タイルが設定(描画)されている座標であるか判定
            if (tilemap.HasTile(cellPosition))
            {
                // スプライトを取得
                Sprite currentSprite = tilemap.GetSprite(cellPosition);

                // スプライトが存在するか確認
                if (currentSprite != null)
                {
                    Debug.Log("現在のスプライト: " + currentSprite.name);
                }
                else
                {
                    Debug.Log("スプライトが設定されていないタイル");
                }
                // プレイヤーの位置をVector3Intに変換して比較
                Vector3Int playerPosition = new Vector3Int(Mathf.FloorToInt(player.transform.position.x), Mathf.FloorToInt(player.transform.position.y), Mathf.FloorToInt(player.transform.position.z));

                if (cellPosition == playerPosition)
                {
                    if (currentSprite == startTile)
                    {
                        Debug.Log("スタートマス");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == goleTile)
                    {
                        Debug.Log("ゴールマス");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == sprite)
                    {
                        Debug.Log("設定忘れ");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == nikuTile)
                    {
                        Debug.Log("肉食材マス");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == sakanaTile)
                    {
                        Debug.Log("魚食材マス");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == yasaiTile)
                    {
                        Debug.Log("野菜食材マス");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == hazureTile)
                    {
                        Debug.Log("ハズレ食材マス");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == bunnkiTile)
                    {
                        Debug.Log("分岐マス");
                        yield return null;
                        break;
                    }
                    else if (currentSprite == eventTile)
                    {
                        Debug.Log("イベントマス");
                        yield return null;
                        break;
                    }
                    else
                    {
                        Debug.Log("どれ？");
                        yield return null;
                        break;
                    }
                }
                else
                {
                    Debug.Log("どこ～？\nマス" + cellPosition + "\nプレイヤー" + playerPosition);

                    yield return null;
                }
            }
        }
    }
}
