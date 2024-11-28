using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Vector3[] routes; // �}�b�v��̃��[�g���i�e�}�X�̈ʒu���i�[�j

    public Vector3 GetTilePosition(int tileIndex)
    {
        if (tileIndex >= 0 && tileIndex < routes.Length)
        {
            return routes[tileIndex];
        }
        else
        {
            Debug.LogWarning($"�����ȃ}�X�ԍ� {tileIndex}");
            return Vector3.zero;
        }
    }

    public int[] GetPossibleRoutes(int currentTileIndex)
    {
        Debug.Log($"���݈ʒu {currentTileIndex} ����I���\�ȃ��[�g���擾");
        // ����_�őI���\�ȃ}�X�ԍ���Ԃ��i����2�̑I�������Œ�j
        return new int[] { currentTileIndex + 1, currentTileIndex + 2 };
    }

    public bool IsKeyRoute(int tileIndex)
    {
        // ����̃}�X�������[�g�ł��邩����i���j
        return tileIndex % 10 == 0; // ����10�}�X���Ƃ������[�g�Ƃ���
    }
}
