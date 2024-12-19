using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapToMasuDB : MonoBehaviour
{
    public Tilemap tilemap;
    private MasuDB masuDB; // MasuDB�̃C���X�^���X��ǉ�

    public Tile startTile;
    public Tile goalTile;
    public Tile meatTile;
    public Tile vegetableTile;
    public Tile fishTile;
    public Tile hazureTile;
    public Tile sonotaTile;
    public Tile eventTile;
    public Tile bunnkiTile;

    void Start()
    {
        if (tilemap == null || masuDB == null)
        {
            Debug.LogError("Tilemap�܂���MasuDB���ݒ肳��Ă��܂���I");
            return;
        }

        // �^�C���}�b�v�̑S�^�C�����擾
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);

            if (tile != null)
            {
                // �ʒu���C���f�b�N�X�ɕϊ�
                int index = position.x + position.y * tilemap.size.x;

                // �^�C���ɑΉ�����C�x���g��ݒ�
                EventType eventType = GetEventTypeFromTile(tile);

                // MasuData�̃C���X�^���X���쐬
                MasuData masu = new MasuData(index, eventType);

                // �K�v�ɉ����ĕ����̃C���f�b�N�X��ݒ�
                // �����ł͉��ɉE�Ɖ��̃^�C���𕪊��ɐݒ肷���
                List<int> bunki = new List<int>();
                if (tilemap.GetTile(new Vector3Int(position.x + 1, position.y, 0)) != null)
                {
                    bunki.Add(index + 1);
                }
                if (tilemap.GetTile(new Vector3Int(position.x, position.y + 1, 0)) != null)
                {
                    bunki.Add(index + tilemap.size.x);
                }
                //masu.bunki = bunki;

                // MasuDB�Ƀ}�X�f�[�^��ǉ�
                masuDB.data.Add(masu);
            }
        }
    }

    // �^�C������EventType�𔻕ʂ��郁�\�b�h
    EventType GetEventTypeFromTile(TileBase tile)
    {
        if (tile == startTile)
        {
            return EventType.Start;
        }
        else if (tile == goalTile)
        {
            return EventType.Goal;
        }
        else if (tile == meatTile)
        {
            return EventType.Meat;
        }
        else if (tile == vegetableTile)
        {
            return EventType.Vegetable;
        }
        else if (tile == fishTile)
        {
            return EventType.Fish;
        }
        else if (tile == hazureTile)
        {
            return EventType.Lose;
        }
        else if (tile == sonotaTile)
        {
            return EventType.Other;
        }
        else if (tile == bunnkiTile)
        {
            return EventType.Branch;
        }
        else if (tile == eventTile)
        {
            return EventType.RandomExchange;
        }
        else
        {
            return EventType.None;
        }
    }
}
