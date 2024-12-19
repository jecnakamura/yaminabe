using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
class MasuData
{
    public int index;            // マスのインデックス
    public EventType ev;         // マスに関連するイベントの種類
    public List<int> bunki;      // 分岐先インデックスのリスト

    public MasuData(int index, EventType ev, List<int> bunki = null)
    {
        this.index = index;
        this.ev = ev;
        this.bunki = bunki ?? new List<int>();
    }
}

class MasuDB
{
    public List<MasuData> data;  // 全てのマスのデータ
    public int nowIndex;         // 現在の位置

    public MasuDB()
    {
        data = new List<MasuData>();
        nowIndex = 0;
    }

    // 指定したオフセットからマス情報を取得
    public List<MasuData> GetOffsetMasuData(int offset)
    {
        List<MasuData> result = new List<MasuData>();
        int targetIndex = nowIndex + offset;

        // 範囲外の場合
        if (targetIndex < 0 || targetIndex >= data.Count)
        {
            Debug.LogWarning("マスの範囲を超えました。");
            return result;
        }

        // 指定インデックスのマスデータを取得
        MasuData masu = GetMasuData(targetIndex);
        result.Add(masu);

        // 分岐がある場合、分岐先のマスデータを追加
        if (masu.bunki != null && masu.bunki.Count > 0)
        {
            foreach (int bunkiIndex in masu.bunki)
            {
                MasuData bunkiMasu = GetMasuData(bunkiIndex);
                if (bunkiMasu != null)
                {
                    result.Add(bunkiMasu);
                }
            }
        }

        return result;
    }

    // 指定したインデックスのマス情報を取得
    public MasuData GetMasuData(int index)
    {
        if (index < 0 || index >= data.Count)
        {
            Debug.LogError($"インデックス {index} が範囲外です。");
            return null;
        }
        return data[index];
    }
}

public enum EventType
{
    None,            // イベントなし
    Meat,            // 肉イベント
    Vegetable,       // 野菜イベント
    Fish,            // 魚イベント
    Other,           // その他のイベント
    Lose,            // ハズレイベント
    RandomExchange,  // ランダムイベント（食材や位置の交換）
    Branch,          // 分岐イベント
    Start,           // スタートマス
    Goal,            // ゴールマス
}
