using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public List<TextMeshProUGUI> rankingTexts; // �v���C���[���̏��ʃe�L�X�g�\���p���X�g
    public Button restartButton; // ������x�V�ԃ{�^��
    public Button quitButton;    // �I���{�^��

    public List<Image> potImage;  // ��̃C���X�g�p

    public Sprite meatPotSprite; // ���̓�C���X�g
    public Sprite fishPotSprite; // ���̓�C���X�g
    public Sprite vegetablePotSprite; // ��؂̓�C���X�g
    public Sprite defaultPotSprite; // �f�t�H���g�̓�C���X�g

    void Start()
    {
        // GameManager ����v���C���[�f�[�^���擾
        List<Player> players = GameManager.Instance.GetPlayers();

        if (players == null || players.Count == 0)
        {
            Debug.LogError("�v���C���[�f�[�^���ݒ肳��Ă��܂���");
            return;
        }

        int maxPlayers = 4;
        int activePlayerCount = GameData.playerCount;

        // �v���C���[�ȊO��UI�͔�\��
        for (int i = activePlayerCount; i < maxPlayers; i++)
        {
            potImage[i].gameObject.SetActive(false);
            rankingTexts[i].gameObject.SetActive(false);
        }

        // �X�R�A���ɕ��ёւ��i�~���j
        players.Sort((p1, p2) => p2.CalculateScore().CompareTo(p1.CalculateScore()));

        // �����L���O��\��
        DisplayRanking(players);

        // �{�^���̃C�x���g��ݒ�
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);

        // ��̉摜��ݒ�
        UpdatePotImages(players);
    }

    public void DisplayRanking(List<Player> players)
    {
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

            // �v���C���[�̏��ʂƃX�R�A��Ή�����e�L�X�g�ɐݒ�
            if (i < rankingTexts.Count)
            {
                rankingTexts[i].text = $"{rank}��: �v���C���[{player.ID + 1} - �X�R�A: {currentScore:F2}";
            }

            previousScore = currentScore; // �O��̃X�R�A���X�V
        }
    }

    void UpdatePotImages(List<Player> players)
    {
        // �e�v���C���[���Ƃɓ�̉摜��ݒ�
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            string mostFrequentRoulette = player.GetMostFrequentRoulette();

            // �ł������񂳂ꂽ���[���b�g�ɑΉ�������I��
            Sprite selectedPotSprite = defaultPotSprite; // �f�t�H���g�̓�ɐݒ�

            switch (mostFrequentRoulette)
            {
                case "Meat":
                    selectedPotSprite = meatPotSprite;
                    break;
                case "Fish":
                    selectedPotSprite = fishPotSprite;
                    break;
                case "Vegetable":
                    selectedPotSprite = vegetablePotSprite;
                    break;
                default:
                    selectedPotSprite = defaultPotSprite;
                    break;
            }

            // �v���C���[�̏��ʂɊ�Â��ē��ݒ� (1�� -> potImage[0], 2�� -> potImage[1] ��)
            if (i < potImage.Count)
            {
                potImage[i].sprite = selectedPotSprite;
            }
        }
    }

    void RestartGame()
    {
        // �X�^�[�g�V�[���ɖ߂�
        SceneManager.LoadScene("TiteScens");
    }

    void QuitGame()
    {
        // �A�v���P�[�V�������I��
        Application.Quit();
    }
}
