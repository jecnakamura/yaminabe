using UnityEngine;

public class TileManeger : MonoBehaviour
{
    public Vector3 RayGoal;

    public void GetTile(Player player)
    {
        var goal = player.camera.WorldToScreenPoint(RayGoal);
        Ray ray = player.camera.ScreenPointToRay(goal);
        int layerMask = 0xffff;
        var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 100, layerMask);

        if (hit.collider != null)
        {
            // �Փ˂����I�u�W�F�N�g�̐F���擾
            var hitObject = hit.collider.gameObject;
            Renderer renderer = hitObject.GetComponent<Renderer>();

            if (renderer != null)
            {
                // �^�C���̐F���擾�i�����_���[�����݂���ꍇ�j
                Color tileColor = renderer.material.color;
                Debug.Log("Hit object color: " + tileColor);

                // �����Ń^�C���̐F�Ɋ�Â��ď��������s�ł��܂�
                if (tileColor == Color.red)
                {
                    Debug.Log("�Ԃ��^�C���ɓ�����܂���");
                }
                else if (tileColor == Color.blue)
                {
                    Debug.Log("���^�C���ɓ�����܂���");
                }
                // ���̑��̐F�̏ꍇ�̏������ǉ��\
            }
        }
    }

}
