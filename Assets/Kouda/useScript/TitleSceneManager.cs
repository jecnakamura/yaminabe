using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
public class TitleSceneManager : MonoBehaviour
{
    private int selectedPlayerCount = 0;     // �I�����ꂽ�v���C���[�l��
    private int maxPlayers = 4;              // �ő�v���C���[���i�Œ�j

    public void Start()
    {
        GameData.playerCount = 0;
    }

    // �v���C���[�l�����{�^���őI�����郁�\�b�h
    public void SelectPlayerCount(int playerCount)
    {
        if (playerCount > 0 && playerCount <= maxPlayers)
        {
            selectedPlayerCount = playerCount;
            Debug.Log($"�I�����ꂽ�v���C���[�l��: {selectedPlayerCount}");
            StartGame();
        }
        else
        {
            Debug.LogWarning("�����ȃv���C���[�l�����I������܂���");
        }
    }

    // �Q�[���J�n�{�^��
    public void StartGame()
    {
        if (selectedPlayerCount > 0)
        {
            GameData.playerCount = selectedPlayerCount;  // �I�����ꂽ�l����ۑ�
            SceneManager.LoadScene("CharacterSelectionScene");  // �L�����N�^�[�I���V�[���֑J��
        }
        else
        {
            Debug.LogWarning("�v���C���[�l����I�����Ă�������");
        }
    }

    // ���[��������ʂ֑J��
    public void GoToRules()
    {
        SceneManager.LoadScene("RuleScene");  // ���[�������V�[���֑J��
    }
}
*/