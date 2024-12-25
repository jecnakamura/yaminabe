using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;           // Tilemap�R���|�[�l���g
    public Grid grid;                 // Grid�R���|�[�l���g
    public float moveSpeed = 5f;      // �ړ����x
    private Vector3 targetPosition;   // �ڕW�ʒu
    private bool isMoving = false;    // �ړ������ǂ����̃t���O

    void Start()
    {
        targetPosition = transform.position; // �����ʒu��ݒ�
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
        // �L�[���͂ňړ�
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
        Vector3Int currentCell = tilemap.WorldToCell(transform.position); // ���݂̃^�C���ʒu���擾
        Vector3Int targetCell = currentCell + direction; // �ڕW�^�C�����v�Z

        if (tilemap.HasTile(targetCell)) // �ړ���̃^�C�������݂��邩�`�F�b�N
        {
            targetPosition = tilemap.CellToWorld(targetCell); // �ڕW�ʒu�����[���h���W�ɕϊ�
            StartCoroutine(MovePlayer());
        }
    }

    IEnumerator MovePlayer()
    {
        isMoving = true;

        // ���݈ʒu����ڕW�ʒu�Ɍ������Ĉړ�
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ŏI�ʒu�ɓ��B
        transform.position = targetPosition;
        isMoving = false;
    }
}
