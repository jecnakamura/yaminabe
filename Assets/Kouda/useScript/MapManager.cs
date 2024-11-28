using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform[] mapTiles; // �}�b�v��̃^�C���i�ʒu�j

    // �v���C���[�����̃}�X�Ɉړ�������
    public void MovePlayer(int playerIndex, int steps, System.Action onComplete)
    {
        Debug.Log($"�v���C���[ {playerIndex + 1} �� {steps} �}�X�ړ������܂�");
        // �v���C���[�̈ړ����W�b�N�������Ɏ����i��: �A�j���[�V�����A�C�x���g�����Ȃǁj

        // �ړ�������A�R�[���o�b�N���Ăяo��
        onComplete?.Invoke();
    }

    // ����̃}�X�ɉ������C�x���g�����s
    public void TriggerTileEvent(int tileIndex, int playerIndex)
    {
        Debug.Log($"�}�X {tileIndex} �Ńv���C���[ {playerIndex + 1} �̃C�x���g�����s");
        // �C�x���g�����������Ɏ���
    }
}
