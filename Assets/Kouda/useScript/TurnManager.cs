using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class TurnManager : MonoBehaviour
{
    public FoodRoulette foodRoulette;    // �H�ރ��[���b�g�Ǘ��X�N���v�g
    public MapManager mapManager;        // �}�b�v�Ǘ��X�N���v�g
    public SugorokuGameManager gameManager; // �Q�[���S�̂̊Ǘ��X�N���v�g

    private int currentPlayerIndex;

    // �v���C���[�̃^�[���J�n
    public void StartPlayerTurn(int playerIndex)
    {
        currentPlayerIndex = playerIndex;
        Debug.Log($"�v���C���[ {currentPlayerIndex + 1} �̃^�[���J�n");

        // ���[���b�g�J�n
        StartRoulette();
    }

    // ���[���b�g�J�n����
    private void StartRoulette()
    {
        Debug.Log("���[���b�g���J�n���܂�");
        foodRoulette.StartRoulette(OnRouletteFinished);
    }

    // ���[���b�g�I�����̏���
    private void OnRouletteFinished(int steps)
    {
        Debug.Log($"���[���b�g�I��: {steps} �}�X�i��");
        mapManager.MovePlayer(currentPlayerIndex, steps, OnMovementComplete);
    }

    // �ړ��������̏���
    private void OnMovementComplete()
    {
        Debug.Log($"�v���C���[ {currentPlayerIndex + 1} �̈ړ����������܂���");
        EndTurn();
    }

    // �^�[���I������
    private void EndTurn()
    {
        Debug.Log($"�v���C���[ {currentPlayerIndex + 1} �̃^�[���I��");
        gameManager.EndTurn();
    }
}
*/