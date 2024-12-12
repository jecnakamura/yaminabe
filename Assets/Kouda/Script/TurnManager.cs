using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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

    public List<Player> players; // �v���C���[�iNPC�܂ށj���X�g
    private int currentPlayerIndex = 0;
    public MapManager mapManager; // �}�b�v�Ǘ��N���X
    public CameraController cameraController;   //�J�����Ǘ��N���X
    public UIManager uiManager; // UI�Ǘ��N���X

    public List<GameObject> commandButtons;


    private bool isGameFinished = false;

    bool isStateEnd = false;

    [SerializeField] GameObject Pl;
    Vector3 spawnPosition;


    void Start()
    {
        cameraController = GetComponent<CameraController>();
        Vector3 scale = new Vector3(0.25f, 0.25f, 1.0f);
        spawnPosition = new Vector3(-23, 3, 0);
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

        StartCoroutine(TurnCycle());
    }

   

    private IEnumerator TurnCycle()
    {
        Player currentPlayer = players[currentPlayerIndex];
        // UI�Ń^�[������\��
        uiManager.ShowTurnInfo(currentPlayer);
        switch (state)
        {
            case TurnState.CommandSelect:
                {
                    //cameraController.FollowPlayer(currentPlayer);
                    yield return StartCoroutine(HandleCommandSelect(currentPlayer));
                }
                break;

            case TurnState.ViewMap:
                {
                }
                break;

            case TurnState.LookList:
                {
                }
                break;

            case TurnState.Roulette:
                {
                    StartCoroutine(HandleRoulette(currentPlayer));
                }
                break;

            case TurnState.PlayerMove:
                {
                    yield return StartCoroutine(HandlePlayerTurn(currentPlayer));
                }
                break;

            case TurnState.Event:
                {
                    if (CheckAllPlayersFinished())
                    {
                        state = TurnState.End;
                        StartCoroutine(TurnCycle());
                    }
                    
                    NextPlayer();
                }
                break;

            case TurnState.End:
                EndGame();
                yield break;
        }
    }


    public void OnCommandButton(int type)
    {
        isStateEnd = true;
        state = (TurnState)type;
    }

    IEnumerator HandleCommandSelect(Player player)
    {
        foreach(var btn in commandButtons)
        {
            btn.SetActive(true);
        }
        while (!isStateEnd) yield return null;

        foreach (var btn in commandButtons)
        {
            btn.SetActive(false);
        }
        NextState(state);
    }

    private IEnumerator HandleRoulette(Player player)
    {
        RouletteResultHandler.SetEnd(false);

        // ���[���b�g�V�[�����J���Č��ʂ��󂯎��
        yield return SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);
        Debug.Log("���[���b�g�I�[�v��");

        // �I���҂�
        while (!RouletteResultHandler.IsEnd())
        {
            yield return null;
        }

        int result = RouletteResultHandler.GetResult(); // ���̌��ʎ擾�֐�
        player.MoveSteps = result;
        
        yield return new WaitForSeconds(1);
        yield return SceneManager.UnloadSceneAsync("Ruretto");
        Debug.Log("���[���b�g�N���[�Y" + result);
        if (player.MoveSteps == 0)//���[���b�g���񂳂��ɕ����ꍇ
        {
            NextState(TurnState.CommandSelect);
            yield break;
        }

        NextState(TurnState.PlayerMove);
    }

    void NextState(TurnState st)
    {
        state = st;
        isStateEnd = false;
        StartCoroutine(TurnCycle());
    }

    private IEnumerator HandlePlayerTurn(Player player)
    {

        // �}�X�̈ړ�����
        for (int i = 0; i < player.MoveSteps; i++)
        {
            Vector3 targetPos = player.transform.position + new Vector3(3.5f, 0.0f, 0.0f);
            yield return StartCoroutine(mapManager.MovePlayerAnimation(player, targetPos));
        }

        // �~�܂����}�X�̃C�x���g����
        //yield return StartCoroutine(mapManager.HandleTileEvent(player, players));

        
        yield return null;
        player.MoveSteps = 0;
        NextState(TurnState.Event);
    }

    private bool CheckAllPlayersFinished()
    {
        return players.TrueForAll(player => player.HasFinished);
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        NextState(TurnState.CommandSelect);
    }

    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("�Q�[���I���I");
        // ���ʔ��\�V�[���Ɉڍs
        SceneManager.LoadScene("ResultScene");
    }

    
}
