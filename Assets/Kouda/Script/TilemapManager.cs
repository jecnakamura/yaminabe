using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goalTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile, eventTile;
    public MasuDB masuDB = new MasuDB(); // MasuDB�̃C���X�^���X��ǉ�

    static TilemapManager instance = null;
    public static TilemapManager Instance { get { return instance; } }

    public RouletteController rouletteController = new RouletteController();

    void Start()
    {
        // �K�v�ȏ����������������ɒǉ�
        instance = this;
    }

    public IEnumerator TileEvent(Player player)
    {

        // �v���C���[�̈ʒu���Z�����W�ɕϊ�
        Vector3Int playerCell = tilemap.WorldToCell(player.transform.position);
        TileBase currentTile = tilemap.GetTile(playerCell);

        if (currentTile != null)
        {
            Debug.Log("���݂̃^�C��: " + currentTile.name);

            // �^�C���ɉ���������
            if (currentTile == startTile)
            {
                Debug.Log("�X�^�[�g�}�X");
            }
            else if (currentTile == goalTile)
            {
                Debug.Log("�S�[���}�X");
            }
            else if (currentTile == nikuTile)
            {
                Debug.Log("���H�ރ}�X");
                //NIKU.GetNIKU(player.Ingredient);
                yield return StartCoroutine(HandleMeatEvent(player));
            }
            else if (currentTile == sakanaTile)
            {
                Debug.Log("���H�ރ}�X");
                yield return StartCoroutine(HandleFishEvent(player));
            }
            else if (currentTile == yasaiTile)
            {
                Debug.Log("��ؐH�ރ}�X");
                yield return StartCoroutine(HandleVegetableEvent(player));
            }
            else if (currentTile == hazureTile)
            {
                Debug.Log("�n�Y���H�ރ}�X");
                yield return StartCoroutine(HandleLoseEvent(player));
            }
            else if (currentTile == bunnkiTile)
            {
                Debug.Log("����}�X");
                yield return StartCoroutine(HandleBranchEvent(player));
            }
            else if (currentTile == eventTile)
            {
                Debug.Log("�C�x���g�}�X");
                yield return StartCoroutine(HandleOtherEvent(player));
            }
            else
            {
                Debug.Log("�s���ȃ^�C��");
            }
        }
        else
        {
            Debug.Log("�^�C����������܂���ł����B");
        }

        // �v���C���[�̌��݈ʒu�ɑΉ�����}�X�����擾
        MasuData masu = masuDB.GetMasuData(player.nowIndex); // �v���C���[�̈ʒu�ɑΉ�����MasuData���擾
        if (masu != null)
        {
            Debug.Log($"�}�X {masu.index} �̃C�x���g������");

            // �}�X�Ɋ֘A����C�x���g�^�C�v���m�F
            switch (masu.ev)
            {
                case EventType.Meat:
                    yield return StartCoroutine(HandleMeatEvent(player));
                    break;

                case EventType.Vegetable: 
                    yield return StartCoroutine(HandleVegetableEvent(player));
                    break;

                case EventType.Fish:
                    yield return StartCoroutine(HandleFishEvent(player));
                    break;

                case EventType.Other:
                    yield return StartCoroutine(HandleOtherEvent(player));
                    break;

                case EventType.Lose:
                    yield return StartCoroutine(HandleLoseEvent(player));
                    break;

                case EventType.RandomExchange:
                    yield return StartCoroutine(HandleRandomEvent(player));
                    break;

                case EventType.Branch:
                    yield return StartCoroutine(HandleBranchEvent(player));
                    break;

                case EventType.Start:
                    Debug.Log("�X�^�[�g�}�X");
                    break;

                case EventType.Goal:
                    Debug.Log("�S�[���}�X");
                    break;

                default:
                    Debug.LogWarning($"����`�̃C�x���g�^�C�v {masu.ev} ���������܂���");
                    break;
            }
        }
        else
        {
            Debug.LogWarning("�v���C���[�̈ʒu�ɑΉ�����}�X�f�[�^��������܂���ł���");
        }
    }

    // �e�C�x���g�̏���
    private IEnumerator HandleMeatEvent(Player player)
    {
        Debug.Log("���C�x���g�������I");
        // ���Ɋ֘A���鏈���������ɋL�q
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleVegetableEvent(Player player)
    {
        Debug.Log("��؃C�x���g�������I");

        int num = Random.Range(1, 3);
        string scenename = "Yasai" + num.ToString() + "RurettoScene";
        
        yield return SceneManager.LoadSceneAsync(scenename);
        player.camera.gameObject.SetActive(false);

        rouletteController.PlayerResult(player);

        yield return new WaitForSeconds(1);

        yield return SceneManager.UnloadSceneAsync(scenename);
        player.camera.gameObject.SetActive(true);


    }

    private IEnumerator HandleFishEvent(Player player)
    {
        Debug.Log("���C�x���g�������I");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleOtherEvent(Player player)
    {
        Debug.Log("���̑��C�x���g�������I");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleLoseEvent(Player player)
    {
        Debug.Log("�n�Y���C�x���g�������I");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleRandomEvent(Player player)
    {
        Debug.Log("�����_���C�x���g�������I");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleBranchEvent(Player player)
    {
        Debug.Log("����C�x���g�������I");
        yield return new WaitForSeconds(1);
    }
}