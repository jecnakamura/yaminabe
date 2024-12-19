using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[Serializable]
public class MasuData
{
    public int index;            // �}�X�̃C���f�b�N�X
    public EventType ev;         // �}�X�Ɋ֘A����C�x���g�̎��
    public List<int> next;      // �����C���f�b�N�X�̃��X�g

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
    public List<MasuData> data;  // �S�Ẵ}�X�̃f�[�^
    //public int nowIndex;         // ���݂̈ʒu

    class Root
    {
        int index;          // �}�X�C���f�b�N�X
        int moveRemain;     // �c��̈ړ������

        public Root(int i, int m) { index = i; moveRemain = m; }

        // �ċA�������g���ďI���_�̃��X�g�����
        public List<int> GetDestinationIndexList(MasuDB db)
        {
            List<int> ret = new List<int>();

            // ����ȏ�ړ��ł��Ȃ�
            if (moveRemain <= 0) return ret;

            // ���݂̃}�X���
            var masu = db.data
                .Where(it => it.index == index)
                .FirstOrDefault();
            if (masu == null) return ret;

            // 1�}�X�i�߂�
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

    // �w�肵���I�t�Z�b�g����}�X�����擾
    public List<MasuData> GetOffsetMasuData(int offset, int nowIndex)
    {
        List<MasuData> result = new List<MasuData>();

        // �s�惊�X�g�쐬
        var root = new Root(nowIndex, offset);
        var indexList = root.GetDestinationIndexList(this);

        // �s�惊�X�g����MasuData�̃��X�g�𐶐�
        List<MasuData> ret = new List<MasuData>();
        foreach(var i in indexList)
        {
            MasuData masu = GetMasuData(i);
            ret.Add(masu);
        }
        return ret;
    }

    // �w�肵���C���f�b�N�X�̃}�X�����擾
    public MasuData GetMasuData(int index)
    {
        if (index < 0 || index >= data.Count)
        {
            Debug.LogError($"�C���f�b�N�X {index} ���͈͊O�ł��B");
            return null;
        }
        return data[index];
    }
}

public enum EventType
{
    None,            // �C�x���g�Ȃ�
    Meat,            // ���C�x���g
    Vegetable,       // ��؃C�x���g
    Fish,            // ���C�x���g
    Other,           // ���̑��̃C�x���g
    Lose,            // �n�Y���C�x���g
    RandomExchange,  // �����_���C�x���g�i�H�ނ�ʒu�̌����j
    Branch,          // ����C�x���g
    Start,           // �X�^�[�g�}�X
    Goal,            // �S�[���}�X
}
