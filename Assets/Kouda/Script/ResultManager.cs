using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI rankingText; // �����L���O��\������UI�e�L�X�g
    public Button restartButton; // ������x�V�ԃ{�^��
    public Button quitButton;    // �I���{�^��

    void Start()
    {
        // GameManager ����v���C���[�f�[�^���擾
        List<Player> players = GameManager.Instance.GetPlayers();

        if (players == null || players.Count == 0)
        {
            Debug.LogError("�v���C���[�f�[�^���ݒ肳��Ă��܂���");
            return;
        }

        // �����L���O��\��
        DisplayRanking(players);

        // �{�^���̃C�x���g��ݒ�
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void DisplayRanking(List<Player> players)
    {
        string result = "���ʔ��\�I\n";

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            result += $"{i + 1}��: �v���C���[{player.ID} - �X�R�A: {player.CalculateScore():F2}\n";
            result += $"�H��: {player.ingredients.Count}��\n";
        }

        rankingText.text = result;
    }

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
