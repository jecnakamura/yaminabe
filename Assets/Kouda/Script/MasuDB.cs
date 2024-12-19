using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[Serializable]
public class MasuData
{
    public int index;            // マスのインデックス
    public EventType ev;         // マスに関連するイベントの種類
    public List<int> next;      // 分岐先インデックスのリスト

    public MasuData(int index, EventType ev, List<int> bunki = null)
    {
        this.index = index;
        this.ev = ev;
        this.next = bunki ?? new List<int>();
    }
}

[Serializable]
public class MasuDB
{
    public List<MasuData> data;  // 全てのマスのデータ
    //public int nowIndex;         // 現在の位置

    class Root
    {
        int index;          // マスインデックス
        int moveRemain;     // 残りの移動する回数

        public Root(int i, int m) { index = i; moveRemain = m; }

        // 再帰処理を使って終着点のリストを作る
        public List<int> GetDestinationIndexList(MasuDB db)
        {
            List<int> ret = new List<int>();

            // これ以上移動できない
            if (moveRemain <= 0) return ret;

            // 現在のマス情報
            var masu = db.data
                .Where(it => it.index == index)
                .FirstOrDefault();
            if (masu == null) return ret;

            // 1マス進める
            foreach(var root in masu.next)
            {
                Root newRoot = new Root(root, moveRemain - 1);
                var list = newRoot.GetDestinationIndexList(db);
                if(moveRemain <= 0)
                {
                    ret.AddRange(list);
                }
            }

            return ret;
        }
    }

    public MasuDB()
    {
        data = new List<MasuData>();
        //nowIndex = 0;
    }

    // 指定したオフセットからマス情報を取得
    public List<MasuData> GetOffsetMasuData(int offset, int nowIndex)
    {
        List<MasuData> result = new List<MasuData>();

        // 行先リスト作成
        var root = new Root(nowIndex, offset);
        var indexList = root.GetDestinationIndexList(this);

        // 行先リストからMasuDataのリストを生成
        List<MasuData> ret = new List<MasuData>();
        foreach(var i in indexList)
        {
            MasuData masu = GetMasuData(i);
            ret.Add(masu);
        }
        return ret;
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
