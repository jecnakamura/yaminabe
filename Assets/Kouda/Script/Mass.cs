using UnityEngine;

public enum MassEventType
{
    None,           // �C�x���g�Ȃ�
    AddIngredient,  // �����_���ȐH�ނ̒ǉ�
    RemoveIngredient, // �����_���ȐH�ނ̍폜
    SwapPositions,  // �v���C���[�����̏ꏊ�����ւ���
    GrantKey,       // ���̕t�^
    MiniGame,       // �~�j�Q�[��
    Goal            // �S�[��
}

public class Mass : MonoBehaviour
{
    public MassEventType eventType;  // ���̃}�X�̃C�x���g�̎��
}
