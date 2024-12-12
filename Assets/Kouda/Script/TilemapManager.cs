using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public enum TileType
{
    Start,  // �X�^�[�g�}�X
    Normal, // �ʏ�}�X
    Event,  // �C�x���g�}�X
    Goal    // �S�[���}�X
}

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goleTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile;



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
                if (cellPosition == player.CurrentPosition)
                {
                    if (tilemap.GetSprite(cellPosition) == startTile)
                    {
                        Debug.Log("�X�^�[�g�}�X");

                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == goleTile)
                    {
                        Debug.Log("�S�[���}�X");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == sprite)
                    {
                        Debug.Log("�ݒ�Y��");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == nikuTile)
                    {
                        Debug.Log("���H��");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == sakanaTile)
                    {
                        Debug.Log("���H��");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == yasaiTile)
                    {
                        Debug.Log("��ؐH��");
                        yield return null;

                    }
                    if (tilemap.GetSprite(cellPosition) == hazureTile)
                    {
                        Debug.Log("�n�Y���H��");
                        yield return null;
                    }
                    if (tilemap.GetSprite(cellPosition) == bunnkiTile)
                    {
                        Debug.Log("����");
                        yield return null;
                    }

                }
                else
                {
                    Debug.Log("�ǂ��`�H");
                    yield return null;
                }
            }
        }

    }
}
