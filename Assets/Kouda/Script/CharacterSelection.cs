using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters;  // �S�L�����N�^�[���X�g
    public List<Image> characterImages;          // �e�v���C���[�̃L�����N�^�[�摜
    public List<TextMeshProUGUI> characterNames; // �e�v���C���[�̃L�����N�^�[�� (TextMeshPro�ɕύX)
    public List<Button> nextButtons;             // �u���v�{�^��
    public List<Button> prevButtons;             // �u���v�{�^��
    public List<Button> confirmButtons;          // ����{�^��
    public List<GameObject> npcStrengthSelectors; // NPC�����I��p�̃I�u�W�F�N�g�i�v���C���[�����Ȃ��ꍇ�ɕ\���j

    private int[] currentIndices;                // �v���C���[���Ƃ̌��݃C���f�b�N�X
    private int maxPlayers = 4;
    private int activePlayerCount;

    //test
    public Sprite characterImage1;
    public Sprite characterImage2;
    public Sprite characterImage3;
    public Sprite characterImage4;
    private void Start()
    {
        // GameData����v���C���[�l�����擾
        activePlayerCount = GameData.playerCount;

        if (activePlayerCount <= 0 || activePlayerCount > maxPlayers)
        {
            Debug.LogError("�s���ȃv���C���[�l���ł��B�f�t�H���g�l���g�p���܂��B");
            activePlayerCount = maxPlayers; // �f�t�H���g�l�Ƀt�H�[���o�b�N
        }
        // �v���C���[�����̃C���f�b�N�X��������
        currentIndices = new int[maxPlayers];
        // �T���v���L�����N�^�[���쐬���Ēǉ�
        availableCharacters = new List<Character>
    {
        new Character { name = "Character1", image = characterImage1 },
        new Character { name = "Character2", image = characterImage2 },
        new Character { name = "Character3", image = characterImage3 },
        new Character { name = "Character4", image = characterImage4 }
    };
        // �����\����ݒ�
        for (int i = 0; i < maxPlayers; i++)
        {
            if (i < activePlayerCount)
            {
                // �A�N�e�B�u�ȃv���C���[�̏ꍇ�A�L�����N�^�[�����蓖�Ă�
                currentIndices[i] = i % availableCharacters.Count;
                characterImages[i].gameObject.SetActive(true);
                characterNames[i].gameObject.SetActive(true);
                nextButtons[i].gameObject.SetActive(true);
                prevButtons[i].gameObject.SetActive(true);
                confirmButtons[i].gameObject.SetActive(true);
                npcStrengthSelectors[i].SetActive(false); // NPC�I��UI�͔�\��
            }
            else
            {
                // �v���C���[�����Ȃ��ꍇ�ANPC�����I����\��
                characterImages[i].gameObject.SetActive(false);
                characterNames[i].gameObject.SetActive(false);
                nextButtons[i].gameObject.SetActive(false);
                prevButtons[i].gameObject.SetActive(false);
                confirmButtons[i].gameObject.SetActive(false);
                npcStrengthSelectors[i].SetActive(true);
            }
        }
        // �L�����N�^�[�\�����X�V
        UpdateCharacterDisplay();
    }

    // �u���v�{�^���������ꂽ�Ƃ�
    public void ShowNextCharacter(int playerIndex)
    {
        currentIndices[playerIndex] = (currentIndices[playerIndex] + 1) % availableCharacters.Count;
        UpdateCharacterDisplay();
    }

    // �u���v�{�^���������ꂽ�Ƃ�
    public void ShowPreviousCharacter(int playerIndex)
    {
        currentIndices[playerIndex] = (currentIndices[playerIndex] - 1 + availableCharacters.Count) % availableCharacters.Count;
        UpdateCharacterDisplay();
    }

    // �L�����N�^�[���m��
    public void ConfirmCharacter(int playerIndex)
    {
        Character selected = availableCharacters[currentIndices[playerIndex]];
        Debug.Log($"Player {playerIndex + 1} has selected {selected.name}");
        // �m��L�����N�^�[�̐ݒ菈����ǉ�
    }

    // NPC�̋�����ݒ�
    public void SetNPCStrength(int npcIndex, NPCStrength strength)
    {
        // NPC�̋�����ݒ肷�鏈��
        Debug.Log($"NPC {npcIndex + 1} �̋����� {strength} �ɐݒ肳��܂���");
    }

    private void UpdateCharacterDisplay()
    {
        if (availableCharacters == null || availableCharacters.Count == 0)
        {
            Debug.LogError("�L�����N�^�[���X�g����ł��B�L�����N�^�[��ݒ肵�Ă��������B");
            return; // �G���[���
        }

        for (int i = 0; i < maxPlayers; i++)
        {
            // �C���f�b�N�X�����X�g�͈͓̔������m�F
            if (currentIndices[i] >= 0 && currentIndices[i] < availableCharacters.Count)
            {
                Character currentCharacter = availableCharacters[currentIndices[i]];
                characterImages[i].sprite = currentCharacter.image;
                characterNames[i].text = currentCharacter.name;
            }
            else
            {
                Debug.LogWarning($"Player {i + 1} �̃C���f�b�N�X���͈͊O�ł��B");
            }
        }
    }
}
