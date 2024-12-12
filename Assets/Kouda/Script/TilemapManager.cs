using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goleTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile, eventTile;



    void Start()
    {

    }

    public IEnumerator TileEvent(Player player)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // ���o�����ʒu��񂩂�^�C���}�b�v�p�̈ʒu���(�Z�����W)���擾
            Vector3Int cellPosition = new Vector3Int(pos.x, pos.y, pos.z);

            // tilemap.HasTile -> �^�C�����ݒ�(�`��)����Ă�����W�ł��邩����
            if (tilemap.HasTile(cellPosition))
            {
                // �v���C���[�̈ʒu��Vector3Int�ɕϊ����Ĕ�r
                Vector3Int playerPosition = new Vector3Int(Mathf.FloorToInt(player.transform.position.x), Mathf.FloorToInt(player.transform.position.y), Mathf.FloorToInt(player.transform.position.z));

                if (cellPosition == playerPosition)
                {
                    if (tilemap.GetSprite(cellPosition) == startTile)
                    {
                        Debug.Log("�X�^�[�g�}�X");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == goleTile)
                    {
                        Debug.Log("�S�[���}�X");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == sprite)
                    {
                        Debug.Log("�ݒ�Y��");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == nikuTile)
                    {
                        Debug.Log("���H�ރ}�X");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == sakanaTile)
                    {
                        Debug.Log("���H�ރ}�X");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == yasaiTile)
                    {
                        Debug.Log("��ؐH�ރ}�X");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == hazureTile)
                    {
                        Debug.Log("�n�Y���H�ރ}�X");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == bunnkiTile)
                    {
                        Debug.Log("����}�X");
                        yield return null;
                        break;
                    }
                    else if (tilemap.GetSprite(cellPosition) == eventTile)
                    {
                        Debug.Log("�C�x���g�}�X");
                        yield return null;
                        break;
                    }
                    else
                    {
                        Debug.Log("�ǂ�H");
                        yield return null;
                        break;
                    }
                }
                else
                {
                    Debug.Log("�ǂ��`�H\n�}�X" + cellPosition + "\n�v���C���[" + playerPosition);

                    yield return null;
                }
            }
        }
    }
}
