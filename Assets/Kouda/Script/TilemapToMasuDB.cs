using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapToMasuDB : MonoBehaviour
{
    public Tilemap tilemap;
    private MasuDB masuDB; // MasuDBのインスタンスを追加

    public Tile startTile;
    public Tile goalTile;
    public Tile meatTile;
    public Tile vegetableTile;
    public Tile fishTile;
    public Tile hazureTile;
    public Tile sonotaTile;
    public Tile eventTile;
    public Tile bunnkiTile;

    void Start()
    {
        if (tilemap == null || masuDB == null)
        {
            Debug.LogError("TilemapまたはMasuDBが設定されていません！");
            return;
        }

        // タイルマップの全タイルを取得
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);

            if (tile != null)
            {
                // 位置をインデックスに変換
                int index = position.x + position.y * tilemap.size.x;

                // タイルに対応するイベントを設定
                EventType eventType = GetEventTypeFromTile(tile);

                // MasuDataのインスタンスを作成
                MasuData masu = new MasuData(index, eventType);

                // 必要に応じて分岐先のインデックスを設定
                // ここでは仮に右と下のタイルを分岐先に設定する例
                List<int> bunki = new List<int>();
                if (tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0)) != null)
                {
                    bunki.Add(index + 1);
                }
                if (tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0)) != null)
                {
                    bunki.Add(index + tilemap.size.x);
                }
                //masu.bunki = bunki;

                // MasuDBにマスデータを追加
                masuDB.data.Add(masu);
            }
        }
    }

    // タイルからEventTypeを判別するメソッド
    EventType GetEventTypeFromTile(TileBase tile)
    {
        if (tile == startTile)
        {
            return EventType.Start;
        }
        else if (tile == goalTile)
        {
            return EventType.Goal;
        }
        else if (tile == meatTile)
        {
            return EventType.Meat;
        }
        else if (tile == vegetableTile)
        {
            return EventType.Vegetable;
        }
        else if (tile == fishTile)
        {
            return EventType.Fish;
        }
        else if (tile == hazureTile)
        {
            return EventType.Lose;
        }
        else if (tile == sonotaTile)
        {
            return EventType.Other;
        }
        else if (tile == bunnkiTile)
        {
            return EventType.Branch;
        }
        else if (tile == eventTile)
        {
            return EventType.RandomExchange;
        }
        else
        {
            return EventType.None;
        }
    }
}
