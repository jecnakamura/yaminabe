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
    public List<Player> players; // �v���C���[���X�g
    private Dictionary<Vector3Int, TileType> tileEvents; // �^�C���C�x���g����
    private Dictionary<Player, Vector3Int> playerPositions; // �e�v���C���[�̌��݈ʒu
    public Sprite sprite, startTile, goleTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile;



    void Start()
    {
        playerPositions = new Dictionary<Player, Vector3Int>();
        InitializeTileEvents();

        // �e�v���C���[���X�^�[�g�}�X�ɔz�u
        foreach (Player player in players)
        {
            Vector3Int startTile = new Vector3Int(0, 0, 0); // �X�^�[�g�}�X�̈ʒu
            player.transform.position = tilemap.CellToWorld(startTile);
            playerPositions[player] = startTile;
        }

        StartCoroutine(GameLoop());
    }

    private void InitializeTileEvents()
    {
        tileEvents = new Dictionary<Vector3Int, TileType>();
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                if (pos == new Vector3Int(0, 0, 0)) // �X�^�[�g�}�X
                {
                    tileEvents[pos] = TileType.Start;
                }
                else if (pos == new Vector3Int(10, 0, 0)) // �S�[���}�X
                {
                    tileEvents[pos] = TileType.Goal;
                }
                else if (pos.x % 5 == 0) // �C�x���g�}�X
                {
                    tileEvents[pos] = TileType.Event;
                }
                else
                {
                    tileEvents[pos] = TileType.Normal;
                }
            }
        }
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            foreach (Player player in players)
            {
                if (player.HasFinished) continue; // �S�[���ς݂̃v���C���[�̓X�L�b�v

                yield return StartCoroutine(HandlePlayerTurn(player));

                if (CheckGameEnd()) yield break;
            }
        }
    }

    private IEnumerator HandlePlayerTurn(Player player)
    {
        Debug.Log($"{player.name} �̃^�[���J�n");

        // ���[���b�g�V�[���ňړ��������擾
        yield return StartCoroutine(OpenRoulette(player));

        // �v���C���[�̈ړ�����
        yield return StartCoroutine(MovePlayer(player));

        // ���݂̃^�C���^�C�v���擾
        Vector3Int currentPos = playerPositions[player];
        if (tileEvents.TryGetValue(currentPos, out TileType tileType))
        {
            // �}�X�̃C�x���g����
            yield return StartCoroutine(HandleTileEvent(tileType, player));
        }
    }

    private IEnumerator OpenRoulette(Player player)
    {
        // ���[���b�g�V�[�����J���Č��ʂ��擾
        yield return SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);

        // ���̌��ʎ擾
        int steps = RouletteResultHandler.GetResult(); // ���[���b�g�̌��ʂ��擾����֐��i����j
        player.MoveSteps = steps;

        yield return SceneManager.UnloadSceneAsync("Ruretto");
    }

    private IEnumerator MovePlayer(Player player)
    {
        int steps = player.MoveSteps;
        Vector3Int startPos = playerPositions[player];

        for (int i = 0; i < steps; i++)
        {
            Vector3Int nextPos = startPos + new Vector3Int(1, 0, 0); // ���ɉE�����Ɉړ�
            if (tilemap.HasTile(nextPos))
            {
                startPos = nextPos;
                player.transform.position = tilemap.CellToWorld(nextPos);
                yield return new WaitForSeconds(0.3f); // �ړ��̃f�B���C
            }
            else
            {
                break;
            }
        }

        playerPositions[player] = startPos; // �ŏI�ʒu���X�V
    }

    private IEnumerator HandleTileEvent(TileType tileType, Player player)
    {
        switch (tileType)
        {
            case TileType.Start:
                Debug.Log("�X�^�[�g�}�X: ���ɃC�x���g�Ȃ�");
                break;

            case TileType.Normal:
                Debug.Log("�ʏ�}�X: �H�ޒǉ��C�x���g����");
                player.AddIngredient(GetRandomIngredient());
                break;

            case TileType.Event:
                Debug.Log("�C�x���g�}�X: �����_���C�x���g����");
                yield return StartCoroutine(HandleRandomEvent(player));
                break;

            case TileType.Goal:
                Debug.Log($"�S�[���}�X: {player.name} ���S�[�����܂����I");
                player.HasFinished = true;
                break;

            default:
                Debug.Log("���m�̃^�C���C�x���g");
                break;
        }
    }

    private IEnumerator HandleRandomEvent(Player player)
    {
        int randomEvent = Random.Range(0, 3);
        switch (randomEvent)
        {
            case 0:
                Debug.Log("�C�x���g: �H�ނ������_����1�폜����܂����I");
                player.RemoveRandomIngredient();
                break;
            case 1:
                Debug.Log("�C�x���g: �v���C���[�̈ʒu����������܂����I");
                SwapPlayers();
                break;
            case 2:
                Debug.Log("�C�x���g: ���v���C���[�ƐH�ތ����I");
                ExchangeIngredients(player, GetRandomOtherPlayer(player));
                break;
        }
        yield return null;
    }

    private Player GetRandomOtherPlayer(Player currentPlayer)
    {
        List<Player> others = players.FindAll(p => p != currentPlayer);
        return others[Random.Range(0, others.Count)];
    }

    private void SwapPlayers()
    {
        // �v���C���[�̈ʒu�������_���Ɍ������郍�W�b�N
    }

    private void ExchangeIngredients(Player player1, Player player2)
    {
        // �v���C���[���m�̐H�ތ������W�b�N
    }

    private bool CheckGameEnd()
    {
        if (players.TrueForAll(player => player.HasFinished))
        {
            Debug.Log("�S�v���C���[���S�[�����܂����I�Q�[���I���I");
            EndGame();
            return true;
        }
        return false;
    }

    private void EndGame()
    {
        Debug.Log("���ʉ�ʂɈڍs���܂��B");
        SceneManager.LoadScene("ResultScene");
    }

    private Ingredient GetRandomIngredient()
    {
        // ���̐H�ސ���
        return new Ingredient("�����_���H��", "���", Random.Range(1, 10), Random.Range(0.5f, 1.5f));
    }

    public void TileEvent(Player player)
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

                        return;
                    }
                    if (tilemap.GetSprite(cellPosition) == goleTile)
                    {
                        Debug.Log("�S�[���}�X");

                    }
                    if (tilemap.GetSprite(cellPosition) == sprite)
                    {
                        Debug.Log("�ݒ�Y��");
                    }
                    if (tilemap.GetSprite(cellPosition) == nikuTile)
                    {
                        Debug.Log("���H��");

                    }
                    if (tilemap.GetSprite(cellPosition) == sakanaTile)
                    {
                        Debug.Log("���H��");

                    }
                    if (tilemap.GetSprite(cellPosition) == yasaiTile)
                    {
                        Debug.Log("��ؐH��");

                    }
                    if (tilemap.GetSprite(cellPosition) == hazureTile)
                    {
                        Debug.Log("�n�Y���H��");

                    }
                    if (tilemap.GetSprite(cellPosition) == bunnkiTile)
                    {
                        Debug.Log("����");

                    }

                }
            }
        }

    }
}
