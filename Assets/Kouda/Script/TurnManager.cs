using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum TurnState
    {
        CommandSelect,
        ViewMap,
        LookList,
        Roulette,
        PlayerMove,
        Event,
        End,
    }

    public TurnState state = TurnState.CommandSelect;
    public Character availableCharacter;    // �S�L�����N�^�[���X�g

    public List<Player> players; // �v���C���[�iNPC�܂ށj���X�g
    private int currentPlayerIndex = 0;
    public MapManager mapManager; // �}�b�v�Ǘ��N���X
    public CameraController cameraController; // �J�����Ǘ��N���X
    public UIManager uiManager; // UI�Ǘ��N���X
    public TilemapManager tilemapManager;
    public List<GameObject> commandButtons;
    public MasuDB masuDB;
    public Button RoulettteGameButton;
    private bool isGameFinished = false;
    private bool isStateEnd = false;

    [SerializeField] GameObject Pl;
    Vector3 spawnPosition;

    void Start()
    {
        InitializePlayers();
        StartCoroutine(TurnCycle());
    }

    private void Update()
    {
        //Debug.Log(state.ToString());
        if(state == TurnState.CommandSelect)
        {
            if(Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Player currentPlayer = players[currentPlayerIndex];
                NextState(TurnState.Roulette);
                StartCoroutine(HandleState(currentPlayer));
            }
        }
    }

    private void InitializePlayers()
    {
        Vector3 scale = new Vector3(0.25f, 0.25f, 1.0f);
        spawnPosition = new Vector3(-23, 3, 0);

        if(GameData.selectedCharacters[0] == null)
        {
            Character selectedCharacter = availableCharacter;

            GameData.selectedCharacters[0] = selectedCharacter;
        }
        for (int i = 0; i < GameData.playerCount; i++)
        {
            var obj = Instantiate(Pl, spawnPosition, Quaternion.identity);
            var player = obj.GetComponent<Player>();
            obj.transform.localScale = scale;

            player.ID = i;
            player.chara = GameData.selectedCharacters[i];
            player.ingredients = new List<Ingredient>();

            players.Add(player);
            player.SetCharaImage();
        }
    }

    private IEnumerator TurnCycle()
    {
        Player currentPlayer = players[currentPlayerIndex];
        ActivateCamera(currentPlayer);
        uiManager.ShowTurnInfo(currentPlayer);

        while (!isGameFinished)
        {
            yield return StartCoroutine(HandleState(currentPlayer));
        }

        EndGame();
    }

    private void ActivateCamera(Player player)
    {
        foreach (var p in players)
        {
            p.camera.gameObject.SetActive(false);
        }
        player.camera.gameObject.SetActive(true);
    }

    private IEnumerator HandleState(Player currentPlayer)
    {
        switch (state)
        {
            case TurnState.CommandSelect:
                RoulettteGameButton.gameObject.SetActive(true);
                yield return StartCoroutine(HandleCommandSelect(currentPlayer));
                break;

            case TurnState.ViewMap:
                yield return StartCoroutine(HandleViewMap());
                break;

            case TurnState.LookList:
                yield return StartCoroutine(HandleLookList());
                break;

            case TurnState.Roulette:
                Debug.Log("Ruretto�V�[�����ǂݍ��܂�܂���");
                yield return StartCoroutine(HandleRoulette(currentPlayer));
                break;

            case TurnState.PlayerMove:
                yield return StartCoroutine(HandlePlayerMove(currentPlayer));
                break;

            case TurnState.Event:
                yield return StartCoroutine(HandleEvent(currentPlayer));
                break;

            case TurnState.End:
                isGameFinished = true;
                break;
        }
    }

    private IEnumerator HandleCommandSelect(Player player)
    {
        ToggleCommandButtons(true);

        
        
        while (!isStateEnd)
        {
            HandleControllerInputForCommand();
            yield return null;
        }

        ToggleCommandButtons(false);
        NextState(state);
    }

    public void OnButtonClick(int type)
    {
        Player currentPlayer = players[currentPlayerIndex];
        NextState((TurnState)type);
        StartCoroutine(HandleState(currentPlayer));
    }

    private void HandleControllerInputForCommand()
    {
        if (Input.GetButtonDown("Submit"))
        {
            // ����{�^���������ꂽ�ꍇ�̏���
            isStateEnd = true;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            // �L�����Z���{�^���ŏ�Ԃ�߂�
            state = TurnState.CommandSelect;
            isStateEnd = true;
        }
    }

    private IEnumerator HandleViewMap()
    {
        while (!isStateEnd)
        {
            HandleControllerInputForViewMap();
            yield return null;
        }

        NextState(TurnState.CommandSelect);
    }

    private void HandleControllerInputForViewMap()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isStateEnd = true;
        }
    }

    private IEnumerator HandleLookList()
    {
        while (!isStateEnd)
        {
            HandleControllerInputForLookList();
            yield return null;
        }

        NextState(TurnState.CommandSelect);
    }

    private void HandleControllerInputForLookList()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isStateEnd = true;
        }
    }

    private IEnumerator HandleRoulette(Player player)
    {
        RoulettteGameButton.gameObject.SetActive(false);

        RouletteResultHandler.SetEnd(false);

        yield return SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);
        
        player.camera.gameObject.SetActive(false);

        while (!RouletteResultHandler.IsEnd())
        {
            HandleControllerInputForRoulette();
            yield return null;
        }

        int result = RouletteResultHandler.GetResult();
        player.MoveSteps = result;

        yield return SceneManager.UnloadSceneAsync("Ruretto");
        player.camera.gameObject.SetActive(true);

        NextState(player.MoveSteps == 0 ? TurnState.CommandSelect : TurnState.PlayerMove);

        StartCoroutine(HandleState(player));
    }

    private void HandleControllerInputForRoulette()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            RouletteResultHandler.SetEnd(true);
        }
    }

    private IEnumerator HandlePlayerMove(Player player)
    {
        for (int i = 0; i < player.MoveSteps; i++)
        {
            Vector3 targetPos = player.transform.position + new Vector3(3.5f, 0.0f, 0.0f);
            yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
            if (tilemapManager.masuDB.data[player.nowIndex].ev == EventType.Branch)
            {
                yield return StartCoroutine(BranchEvent(player));
            }
        }

        yield return StartCoroutine(tilemapManager.TileEvent(player));
        player.MoveSteps = 0;

        NextState(TurnState.Event);
        StartCoroutine(TurnCycle());
    }

    private IEnumerator HandleEvent(Player player)
    {
        tilemapManager.TileEvent(player);
        NextPlayer();
        yield return null;
    }

    private void ToggleCommandButtons(bool isActive)
    {
        foreach (var btn in commandButtons)
        {
            btn.SetActive(isActive);
        }
    }

    private void NextState(TurnState newState)
    {
        state = newState;
        isStateEnd = false;
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        NextState(TurnState.CommandSelect);
    }

    private void EndGame()
    {
        Debug.Log("�Q�[���I���I");
        SceneManager.LoadScene("ResultScene");
    }

    private IEnumerator BranchEvent(Player player)
    {
        // �����}�X���擾
        MasuData currentMasu = masuDB.GetMasuData(player.nowIndex);
        List<int> nextIndices = currentMasu.next;

        if (nextIndices == null || nextIndices.Count == 0)
        {
            Debug.LogWarning("����悪����܂���B");
            yield break;
        }

        // ����I��UI��\��
        uiManager.ShowBranchOptions(nextIndices);

        // �v���C���[���I������܂őҋ@
        int selectedIndex = -1;
        bool isOptionSelected = false;

        // �C�x���g���X�i�[��ǉ�
        uiManager.OnBranchSelected += (index) =>
        {
            selectedIndex = index;
            isOptionSelected = true;
        };

        // �I������������܂őҋ@
        while (!isOptionSelected)
        {
            // ���̕����Ńv���C���[�̈ړ�������ҋ@
            yield return null;
        }

        // �C�x���g���X�i�[������
        uiManager.OnBranchSelected -= (index) =>
        {
            selectedIndex = index;
            isOptionSelected = true;
        };

        // UI���\���ɂ���
        uiManager.HideBranchOptions();

        // �I�����ꂽ�����Ɉړ�
        player.nowIndex = selectedIndex;

        Debug.Log($"�v���C���[ {player.ID} �������I��: �}�X {selectedIndex}");

        // �I�����ꂽ�}�X�Ɉړ��𑱍s
        yield return null;
    }
}
