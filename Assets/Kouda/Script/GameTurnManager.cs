using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public class GameTurnManager : MonoBehaviour
{
    public List<Player> players;  // �v���C���[���X�g
    public MapManager mapManager;  // �}�b�v�Ǘ�
    public UIManager uiManager;    // UI�Ǘ�
    private int currentPlayerIndex = 0;
    private bool isGameFinished = false;

    void Start()
    {
        StartCoroutine(TurnCycle());
    }

    // �^�[�����ɏ��������s
    private IEnumerator TurnCycle()
    {
        while (!isGameFinished)
        {
            Player currentPlayer = players[currentPlayerIndex];
            yield return StartCoroutine(HandlePlayerTurn(currentPlayer));

            if (CheckAllPlayersFinished())
            {
                EndGame();
                yield break;
            }

            NextPlayer();
        }
    }

    // �v���C���[�̃^�[��������
    private IEnumerator HandlePlayerTurn(Player player)
    {
        // UI�Ń^�[������\��
        uiManager.ShowTurnInfo(player);

        // ���[���b�g����
        /*if (!player.IsNPC)
        {
            yield return StartCoroutine(player.RollRoulette());
        }
        else
        {
            player.MoveSteps = Random.Range(1, 7);  // NPC�̓����_���ɃX�e�b�v�i��
        }
        */
        // �}�X�̈ړ�����
        mapManager.MovePlayer(player);

        // �C�x���g����
        yield return StartCoroutine(mapManager.HandleTileEvent(player));
    }

    // �S�����S�[���������m�F
    private bool CheckAllPlayersFinished()
    {
        return players.TrueForAll(player => player.HasFinished);
    }

    // ���̃v���C���[�̃^�[���Ɉڂ�
    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }

    // �Q�[���I������
    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("�Q�[���I���I");
        // ���ʔ��\�V�[���Ɉڍs
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
}