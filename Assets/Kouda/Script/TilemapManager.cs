using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Threading;
using Unity.VisualScripting;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite sprite, startTile, goalTile, nikuTile, sakanaTile, yasaiTile, hazureTile, bunnkiTile, eventTile;
    public MasuDB masuDB = new MasuDB(); // MasuDB�̃C���X�^���X��ǉ�

    static TilemapManager instance = null;
    public static TilemapManager Instance { get { return instance; } }

    public RouletteController rouletteController = new RouletteController();

    private string scenename;

    public Tilemap Tile;

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
        scenename = "NikuRurettoScene";

        yield return FoodRoulette(scenename, player);
    }

    private IEnumerator HandleVegetableEvent(Player player)
    {
        Debug.Log("��؃C�x���g�������I");

        int num = Random.Range(1, 3);
        scenename = "Yasai" + num.ToString() + "RurettoScene";
        
        yield return FoodRoulette(scenename, player);

    }

    private IEnumerator HandleFishEvent(Player player)
    {
        Debug.Log("���C�x���g�������I");
        scenename = "GyokaiRurettoScene";

        yield return FoodRoulette(scenename,player);
    }

    private IEnumerator HandleOtherEvent(Player player)
    {
        Debug.Log("���̑��C�x���g�������I");
        scenename = "SonotaRurettoScene";

        yield return FoodRoulette(scenename,player);

    }

    private IEnumerator HandleLoseEvent(Player player)
    {
        Debug.Log("�n�Y���C�x���g�������I");
        scenename = "HazureRurettoScene";

        yield return FoodRoulette(scenename, player);
    }

    private IEnumerator HandleRandomEvent(Player player)
    {
        Debug.Log("�����_���C�x���g�������I");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator HandleBranchEvent(Player player)
    {
        Debug.Log("����C�x���g�������I");
        yield return new WaitForSeconds(0);
    }

    public IEnumerator FoodRoulette(string scenename,Player player)
    {
        //TilemapRenderer sort = Tile.GetComponent<TilemapManager>();
        var asyncLoad = SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        player.camera.gameObject.SetActive(false);

        rouletteController = GameObject.Find("RouletteController").GetComponent<RouletteController>();
        while (!rouletteController.isFinish)
        {
            yield return null;
        }

        rouletteController.PlayerResult(player);

        yield return new WaitForSeconds(1);

        SceneManager.UnloadSceneAsync(scenename);
        player.camera.gameObject.SetActive(true);
        //Tile.layer = 5;
    }
}