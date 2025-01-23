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

        // �X�R�A���ɕ��ёւ��i�~���j
        players.Sort((p1, p2) => p2.CalculateScore().CompareTo(p1.CalculateScore()));

        // �����L���O��\��
        DisplayRanking(players);

        // �{�^���̃C�x���g��ݒ�
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void DisplayRanking(List<Player> players)
    {
        string result = "���ʔ��\�I\n";

        int rank = 1; // ���ʂ̏����l
        float previousScore = -1; // �O�̃X�R�A�i���_�����p�j

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            float currentScore = player.CalculateScore();

            // ���_�̏ꍇ�͏��ʂ�ύX�����A�O�̏��ʂ�ێ�
            if (currentScore != previousScore)
            {
                rank = i + 1; // �X�R�A���ς�����珇�ʂ��X�V
            }

            result += $"{rank}��: �v���C���[{player.ID + 1} - �X�R�A: {currentScore:F2}\n";
            result += $"�H��: {player.ingredients.Count}��\n";

            previousScore = currentScore; // �O��̃X�R�A���X�V
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
