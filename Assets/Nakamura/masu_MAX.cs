using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masu_MAX : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _cols = 5;
    private void Start()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Instantiate(_tile, new Vector3(row, col, 0),
                    Quaternion.identity, transform);
            }
        }
    }
}
