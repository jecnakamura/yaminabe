using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;           // Tilemapコンポーネント
    public Grid grid;                 // Gridコンポーネント
    public float moveSpeed = 5f;      // 移動速度
    private Vector3 targetPosition;   // 目標位置
    private bool isMoving = false;    // 移動中かどうかのフラグ

    void Start()
    {
        targetPosition = transform.position; // 初期位置を設定
    }

    void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        // キー入力で移動
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToTile(Vector3Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToTile(Vector3Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToTile(Vector3Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToTile(Vector3Int.right);
        }
    }

    void MoveToTile(Vector3Int direction)
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position); // 現在のタイル位置を取得
        Vector3Int targetCell = currentCell + direction; // 目標タイルを計算

        if (tilemap.HasTile(targetCell)) // 移動先のタイルが存在するかチェック
        {
            targetPosition = tilemap.CellToWorld(targetCell); // 目標位置をワールド座標に変換
            StartCoroutine(MovePlayer());
        }
    }

    IEnumerator MovePlayer()
    {
        isMoving = true;

        // 現在位置から目標位置に向かって移動
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最終位置に到達
        transform.position = targetPosition;
        isMoving = false;
    }
}
