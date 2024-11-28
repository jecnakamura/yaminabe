using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public int currentTileIndex = 0; // ���݂̃}�X�ʒu
    public int npcIndex;            // NPC�̔ԍ�

    public void Move(int steps)
    {
        Debug.Log($"NPC {npcIndex + 1} �� {steps} �X�e�b�v�i�݂܂�");

        for (int i = 0; i < steps; i++)
        {
            currentTileIndex++;
            Debug.Log($"NPC {npcIndex + 1} �̓}�X {currentTileIndex} �ɓ��B");
            // �}�X�̃C�x���g������
            HandleTileEvent(currentTileIndex);

            // �S�[������i��: �}�X�����K��l�𒴂����ꍇ�j
            if (IsGoalReached(currentTileIndex))
            {
                Debug.Log($"NPC {npcIndex + 1} ���S�[���ɓ��B���܂���");
                break;
            }
        }
    }

    private void HandleTileEvent(int tileIndex)
    {
        Debug.Log($"�}�X {tileIndex} �̃C�x���g��������");
        // EventManager �ŃC�x���g���g���K�[
        FindObjectOfType<EventManager>().TriggerEvent(tileIndex, npcIndex);
    }

    private bool IsGoalReached(int tileIndex)
    {
        // �S�[�������𔻒�i����100�}�X�ŃS�[���Ƃ���j
        return tileIndex >= 100;
    }
}
