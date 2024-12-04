using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters;    // �S�L�����N�^�[���X�g
    public List<Image> characterImages;            // �e�v���C���[�̃L�����N�^�[�摜
    public List<TextMeshProUGUI> characterNames;   // �e�v���C���[�̃L�����N�^�[��
    public List<Button> nextButtons;               // �u���v�{�^��
    public List<Button> prevButtons;               // �u���v�{�^��
    public List<Button> confirmButtons;            // ����{�^��
    public List<GameObject> npcStrengthSelectors;  // NPC�̋����I���I�u�W�F�N�g
    public Button startGameButton;                 // �Q�[���X�^�[�g�{�^��
    private int[] currentIndices;                  // �e�v���C���[�̌��݃C���f�b�N�X
    private int activePlayerCount;                 // �L���ȃv���C���[��
    private int maxPlayers = 4;
    private bool allPlayersConfirmed = false;      // �v���C���[�S�����L���������肵����
    private bool allNPCStrengthsSet = false;       // �SNPC�̋��������肵����

    private void Start()
    {
        activePlayerCount = GameData.playerCount;  // �ۑ����ꂽ�v���C���[�l�����擾
        currentIndices = new int[maxPlayers];     // �C���f�b�N�X�z���������

        if (availableCharacters == null || availableCharacters.Count == 0)
        {
            Debug.LogError("availableCharacters �ɃL�����N�^�[�f�[�^���ݒ肳��Ă��܂���I");
            return;
        }

        // �v���C���[��NPC��UI��ݒ�
        SetupPlayerUI();

        // NPC�pUI�ƃQ�[���X�^�[�g�{�^���͔�\��
        foreach (var npcSelector in npcStrengthSelectors)
            npcSelector.SetActive(false);

        startGameButton.gameObject.SetActive(false);
    }

    private void SetupPlayerUI()
    {
        // �v���C���[UI��������
        for (int i = 0; i < activePlayerCount; i++)
        {
            currentIndices[i] = i % availableCharacters.Count;

            characterImages[i].gameObject.SetActive(true);
            characterNames[i].gameObject.SetActive(true);
            nextButtons[i].gameObject.SetActive(true);
            prevButtons[i].gameObject.SetActive(true);
            confirmButtons[i].gameObject.SetActive(true);
            currentIndices[i] = currentIndices[i] % availableCharacters.Count;
            UpdateCharacterDisplay(i);
        }

        // �v���C���[�ȊO��UI�͔�\��
        for (int i = activePlayerCount; i < maxPlayers; i++)
        {
            characterImages[i].gameObject.SetActive(false);
            characterNames[i].gameObject.SetActive(false);
            nextButtons[i].gameObject.SetActive(false);
            prevButtons[i].gameObject.SetActive(false);
            confirmButtons[i].gameObject.SetActive(false);
        }
    }

    private void CheckAllPlayersConfirmed()
    {
        allPlayersConfirmed = true;

        for (int i = 0; i < activePlayerCount; i++)
        {
            if (GameData.selectedCharacters[i] == null)
            {
                allPlayersConfirmed = false;
                break;
            }
        }

        if (allPlayersConfirmed)
        {
            Debug.Log("All players confirmed their characters.");
            // �v���C���[��4�l�Ȃ�Q�[���X�^�[�g�{�^����\������
            if (activePlayerCount == maxPlayers)
            {
                startGameButton.gameObject.SetActive(true);
            }
            if (activePlayerCount < maxPlayers)
            {
                AssignNPCCharacters();
            }
        }
    }

    private void AssignNPCCharacters()
    {
        // �v���C���[���I�������L�����N�^�[�����O
        List<Character> unselectedCharacters = new List<Character>(availableCharacters);
        foreach (var character in GameData.selectedCharacters)
        {
            if (character != null)
                unselectedCharacters.Remove(character);
        }

        // NPC�L�����N�^�[�����蓖��
        GameData.npcData.Clear();
        for (int i = 0; i < maxPlayers - activePlayerCount; i++)
        {
            NPCData npc = new NPCData
            {
                assignedCharacter = unselectedCharacters[i],
                npcStrength = NPCStrength.Unset
            };
            GameData.npcData.Add(npc);
            npcStrengthSelectors[i].SetActive(true); // NPC�̋����I��UI��\��
        }
    }

    // NPC�̋�����ݒ肷�郁�\�b�h
    public void SetNPCStrength(int npcIndex, NPCStrength strength)
    {
        if (npcIndex < GameData.npcData.Count)
        {
            GameData.npcData[npcIndex].npcStrength = strength;
            Debug.Log($"NPC {npcIndex} strength set to {strength}");

            // NPC�̋������S�Đݒ肳��Ă��邩�m�F
            CheckAllNPCStrengthsSet();
        }
    }

    private void CheckAllNPCStrengthsSet()
    {
        allNPCStrengthsSet = true;

        foreach (var npc in GameData.npcData)
        {
            if (npc.npcStrength == NPCStrength.Unset)
            {
                allNPCStrengthsSet = false;
                break;
            }
        }

        if (allNPCStrengthsSet)
        {
            Debug.Log("All NPC strengths set. Game can start.");
            startGameButton.gameObject.SetActive(true); // �Q�[���X�^�[�g�{�^����\��
        }
    }

    // NPC�̋����I���{�^���iWeak, Normal, Strong�j�̏���
    public void SetWeak(int npcIndex)
    {
        SetNPCStrength(npcIndex, NPCStrength.Weak);
    }

    public void SetNormal(int npcIndex)
    {
        SetNPCStrength(npcIndex, NPCStrength.Normal);
    }

    public void SetStrong(int npcIndex)
    {
        SetNPCStrength(npcIndex, NPCStrength.Strong);
    }

    private void ShowNextCharacter(int playerIndex)
    {
        int initialIndex = currentIndices[playerIndex];
        do
        {
            currentIndices[playerIndex] = (currentIndices[playerIndex] + 1) % availableCharacters.Count;
        }
        while (IsCharacterAlreadySelected(playerIndex) && currentIndices[playerIndex] != initialIndex);

        UpdateCharacterDisplay(playerIndex);
    }

    private void ShowPreviousCharacter(int playerIndex)
    {
        int initialIndex = currentIndices[playerIndex];
        do
        {
            currentIndices[playerIndex] = (currentIndices[playerIndex] - 1 + availableCharacters.Count) % availableCharacters.Count;
        }
        while (IsCharacterAlreadySelected(playerIndex) && currentIndices[playerIndex] != initialIndex);

        UpdateCharacterDisplay(playerIndex);
    }

    // ���̃v���C���[���I�������L�����N�^�[���ǂ������m�F
    private bool IsCharacterAlreadySelected(int currentPlayerIndex)
    {
        Character currentCharacter = availableCharacters[currentIndices[currentPlayerIndex]];
        for (int i = 0; i < activePlayerCount; i++)
        {
            if (i != currentPlayerIndex && GameData.selectedCharacters[i] == currentCharacter)
            {
                return true;
            }
        }
        return false;
    }

    // ConfirmCharacter ���\�b�h�̏C��
    public void ConfirmCharacter(int playerIndex)
    {
        Character selectedCharacter = availableCharacters[currentIndices[playerIndex]];

        // ���v���C���[���I�����Ă���L�����N�^�[���Ċm�F
        if (GameData.selectedCharacters.Contains(selectedCharacter))
        {
            Debug.Log("���̃v���C���[�����łɑI�����Ă���L�����N�^�[�ł��I");
            return;
        }

        GameData.selectedCharacters[playerIndex] = selectedCharacter;
        Debug.Log($"Player {playerIndex + 1} selected {selectedCharacter.characterName}");

        // ����{�^���𖳌���
        confirmButtons[playerIndex].interactable = false;

        // �S�v���C���[���L���������肵�����m�F
        CheckAllPlayersConfirmed();
    }

    private void UpdateCharacterDisplay(int playerIndex)
    {
        Character currentCharacter = availableCharacters[currentIndices[playerIndex]];
        characterImages[playerIndex].sprite = currentCharacter.image;
        characterNames[playerIndex].text = currentCharacter.characterName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("NSScene");
    }
}
