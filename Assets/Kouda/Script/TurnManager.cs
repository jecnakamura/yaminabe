using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // �v���C���[�̃��X�g�iNPC�܂ށj
    public List<GameObject> players;  // �e�v���C���[�̃I�u�W�F�N�g�i���f�[�^�Ƃ���GameObject���X�g��z��j
    private int currentPlayerIndex = 0;  // ���݂̃v���C���[�̃C���f�b�N�X
    private bool isTurnActive = false;   // �^�[�����i�s�����ǂ������Ǘ�����t���O

    void Start()
    {
        StartCoroutine(TurnCycle());  // �^�[���̃��[�v���J�n
    }

    // �^�[���̃T�C�N�����Ǘ�����R���[�`��
    private IEnumerator TurnCycle()
    {
        while (true)
        {
            yield return StartCoroutine(PlayerTurn(players[currentPlayerIndex])); // ���݂̃v���C���[�̃^�[�����J�n
            EndTurn(); // �^�[���I������
            yield return new WaitForSeconds(1f); // �^�[���Ԃ̃C���^�[�o��
        }
    }

    // �e�v���C���[�̃^�[������
    private IEnumerator PlayerTurn(GameObject player)
    {
        isTurnActive = true;
        Debug.Log($"Player {currentPlayerIndex + 1}'s Turn");

        // �����Ńv���C���[�̃^�[���̏��������s����i�_�C�X��U��A�ړ��Ȃǁj
        // �������Ƃ��Ĉ�莞�ԑҋ@
        yield return new WaitForSeconds(2f);  // �^�[���̉������Ƃ���2�b�ҋ@

        isTurnActive = false;
    }

    // �^�[���I������
    private void EndTurn()
    {
        // ���݂̃v���C���[�̃^�[�����I�������̂Ŏ��̃v���C���[�Ɉړ�
        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;  // �S�v���C���[���^�[�����I������ŏ��ɖ߂�
        }
    }
}
