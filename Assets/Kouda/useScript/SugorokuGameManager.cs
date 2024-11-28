using UnityEngine;

public class SugorokuGameManager : MonoBehaviour
{
    private int currentPlayerIndex = 0; // ���݂̃v���C���[�̃C���f�b�N�X
    private int totalPlayers;          // ���v���C���[��

    public TurnManager turnManager;    // �^�[���Ǘ��X�N���v�g
    public MapManager mapManager;      // �}�b�v�Ǘ��X�N���v�g
    public ScoreManager scoreManager;  // �X�R�A�Ǘ��X�N���v�g

    private void Start()
    {
        InitializeGame();
    }

    // �Q�[���̏�����
    private void InitializeGame()
    {
        totalPlayers = GameData.playerCount;
        if (totalPlayers == 0)
        {
            Debug.LogError("�v���C���[�l�����ݒ肳��Ă��܂���I");
            return;
        }

        Debug.Log($"�Q�[�����J�n���܂��B�v���C���[�l��: {totalPlayers}");
        StartTurn();
    }

    // �^�[���J�n
    private void StartTurn()
    {
        Debug.Log($"�v���C���[ {currentPlayerIndex + 1} �̃^�[���J�n");
        //turnManager.StartPlayerTurn(currentPlayerIndex);
    }

    // �^�[���I�����̏���
    public void EndTurn()
    {
        Debug.Log($"�v���C���[ {currentPlayerIndex + 1} �̃^�[���I��");
        currentPlayerIndex = (currentPlayerIndex + 1) % totalPlayers;

        // �Q�[���I�������̊m�F
        if (CheckGameEnd())
        {
            EndGame();
        }
        else
        {
            StartTurn();
        }
    }

    // �Q�[���I�������̊m�F
    private bool CheckGameEnd()
    {
        // �S�v���C���[���S�[�������ꍇ�ȂǁA�I�������������ɐݒ�
        return false;
    }

    // �Q�[���I�����̏���
    private void EndGame()
    {
        Debug.Log("�Q�[�����I�����܂����B���ʉ�ʂֈڍs���܂��B");
        // �X�R�A�v�Z�������Ăяo��
        //scoreManager.CalculateFinalScores();

        // ���ʉ�ʂւ̑J��
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
}
