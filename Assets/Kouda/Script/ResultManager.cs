using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI rankingText; // �����L���O��\������UI�e�L�X�g
    public Button restartButton; // ������x�V�ԃ{�^��
    public Button quitButton;    // �I���{�^��
    /*
private List<PlayerData> players;


    void Start()
    {
        // GameManager ����v���C���[�f�[�^���擾
        rankedPlayers = GameManager.Instance.GetPlayerData();

        // �����L���O��\��
        DisplayRanking(rankedPlayers);

        // �{�^���̃C�x���g��ݒ�
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void DisplayRanking(List<Player> players)
    {
        string result = "���ʔ��\�I\n";

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            result += $"{i + 1}��: �v���C���[{player.ID} - �X�R�A: {player.CalculateScore():F2}\n";
            result += $"�H��: {player.ingredients.Count}��\n";
        }

        rankingText.text = result;
    }*/
    void RestartGame()
    {
        // �X�^�[�g�V�[���ɖ߂�
        SceneManager.LoadScene("TitleScene");
    }

    void QuitGame()
    {
        // �A�v���P�[�V�������I��
        Application.Quit();
    }
}
