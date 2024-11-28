using UnityEngine;

public class EventManager : MonoBehaviour
{
    // �C�x���g����
    public void TriggerEvent(int tileIndex, int playerIndex)
    {
        Debug.Log($"�}�X {tileIndex} �̃C�x���g���v���C���[ {playerIndex + 1} �ɔ��������܂�");

        // �}�X�̎�ނɉ������C�x���g�����s
        switch (GetTileEventType(tileIndex))
        {
            case TileEventType.None:
                Debug.Log("�C�x���g�Ȃ�");
                break;
            case TileEventType.Bonus:
                ApplyBonus(playerIndex);
                break;
            case TileEventType.Penalty:
                ApplyPenalty(playerIndex);
                break;
            case TileEventType.MiniGame:
                StartMiniGame(playerIndex);
                break;
            default:
                Debug.LogWarning("����`�̃C�x���g�^�C�v");
                break;
        }
    }

    // �}�X�̃C�x���g�^�C�v���擾
    private TileEventType GetTileEventType(int tileIndex)
    {
        // ������: �}�X���Ƃ̃C�x���g�^�C�v��Ԃ��i���j
        return TileEventType.None; // �{���̓}�X���Ƃ̃f�[�^����擾
    }

    // �{�[�i�X�C�x���g
    private void ApplyBonus(int playerIndex)
    {
        Debug.Log($"�v���C���[ {playerIndex + 1} �Ƀ{�[�i�X��K�p");
        // �{�[�i�X�����������i��: �X�R�A���Z�j
    }

    // �y�i���e�B�C�x���g
    private void ApplyPenalty(int playerIndex)
    {
        Debug.Log($"�v���C���[ {playerIndex + 1} �Ƀy�i���e�B��K�p");
        // �y�i���e�B�����������i��: �X�R�A���Z�j
    }

    // �~�j�Q�[���C�x���g
    private void StartMiniGame(int playerIndex)
    {
        Debug.Log($"�v���C���[ {playerIndex + 1} �ƃ~�j�Q�[�����J�n");
        // �~�j�Q�[���V�[���J�ڂȂǂ̏���������
    }
}

// �}�X�̃C�x���g�^�C�v�񋓌^
public enum TileEventType
{
    None,    // �C�x���g�Ȃ�
    Bonus,   // �{�[�i�X
    Penalty, // �y�i���e�B
    MiniGame // �~�j�Q�[��
}
