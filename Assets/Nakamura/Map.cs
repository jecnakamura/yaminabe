using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite;

    public void replaceTilemap()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // âΩÇ©ÇÃèàóù
        }
    }
}
