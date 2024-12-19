using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
class MasuData
{
    public int index;            // �}�X�̃C���f�b�N�X
    public EventType ev;         // �}�X�Ɋ֘A����C�x���g�̎��
    public List<int> bunki;      // �����C���f�b�N�X�̃��X�g

    public MasuData(int index, EventType ev, List<int> bunki = null)
    {
        this.index = index;
        this.ev = ev;
        this.bunki = bunki ?? new List<int>();
    }
}

class MasuDB
{
    public List<MasuData> data;  // �S�Ẵ}�X�̃f�[�^
    public int nowIndex;         // ���݂̈ʒu

    public MasuDB()
    {
        data = new List<MasuData>();
        nowIndex = 0;
    }

    // �w�肵���I�t�Z�b�g����}�X�����擾
    public List<MasuData> GetOffsetMasuData(int offset)
    {
        List<MasuData> result = new List<MasuData>();
        int targetIndex = nowIndex + offset;

        // �͈͊O�̏ꍇ
        if (targetIndex < 0 || targetIndex >= data.Count)
        {
            Debug.LogWarning("�}�X�͈̔͂𒴂��܂����B");
            return result;
        }

        // �w��C���f�b�N�X�̃}�X�f�[�^���擾
        MasuData masu = GetMasuData(targetIndex);
        result.Add(masu);

        // ���򂪂���ꍇ�A�����̃}�X�f�[�^��ǉ�
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
