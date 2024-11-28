using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    //public GameUIManager uiManager; // UI�Ǘ��N���X

    public List<GameObject> commandButtons;

    public GameObject testMoveText;

    private bool isGameFinished = false;

    bool isStateEnd = false;

    [SerializeField] GameObject Pl;
    Vector3 spawnPosition;

    void Start()
    {
        testMoveText.SetActive(false);
        spawnPosition = new Vector3(-23, 0, 0);
        for (int i = 0; i < GameData.playerCount; i++)
        {
            //Player newPlayer = new Player
            //{
            //    ID = i,
            //    chara = GameData.selectedCharacters[i],
            //    ingredients = new List<Ingredient>
            //    {
            //        new Ingredient("", "", 0, 0.0f),
            //    }
            //};
            //players.Add(newPlayer);

            var obj = Instantiate(Pl, spawnPosition, Quaternion.identity);
            var player = obj.GetComponent<Player>();

            player.ID = i;
            player.chara = GameData.selectedCharacters[i];
            player.ingredients = new List<Ingredient>();

            players.Add(player);
        }
        StartCoroutine(TurnCycle());
    }

    private IEnumerator TurnCycle()
    {
        Player currentPlayer = players[currentPlayerIndex];
        switch (state)
        {
            case TurnState.CommandSelect:
                {
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
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);
        Debug.Log("���[���b�g�I�[�v��");

        // �I���҂�
        while (!RouletteResultHandler.IsEnd())
        {
            yield return null;
        }

        int result = RouletteResultHandler.GetResult(); // ���̌��ʎ擾�֐�
        player.MoveSteps = result;
        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ruretto");
        Debug.Log("���[���b�g�N���[�Y" + result);

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
        // UI�Ń^�[������\��
        //uiManager.ShowTurnInfo(player);

        // �}�X�̈ړ�����
        //yield return mapManager.MovePlayer(player,player.MoveSteps);

        // �~�܂����}�X�̃C�x���g����
        //yield return StartCoroutine(mapManager.HandleTileEvent(player, players));

        testMoveText.SetActive(true);
        yield return new WaitForSeconds(2);
        testMoveText.SetActive(false);

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    
}
