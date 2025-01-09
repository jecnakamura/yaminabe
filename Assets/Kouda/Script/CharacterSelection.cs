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
    public List<GameObject> npcStrengthSelectors;  // NPC�̋����I���I�u�W�F�N�g
    public Button startGameButton;                 // �Q�[���X�^�[�g�{�^��
    private int[] currentIndices;                  // �e�v���C���[�̌��݃C���f�b�N�X
    private int activePlayerCount;                 // �L���ȃv���C���[��
    private int maxPlayers = 4;
    private bool[] playerConfirmed;                // �v���C���[�̃L����������
    private Dictionary<int, int> controllerAssignments; // �R���g���[���[�ԍ� -> �v���C���[�ԍ�
    private bool allNPCStrengthsSet = false;       // �SNPC�̋��������肵����

    private void Start()
    {
        activePlayerCount = GameData.playerCount;  // �ۑ����ꂽ�v���C���[�l�����擾
        currentIndices = new int[maxPlayers];     // �C���f�b�N�X�z���������
        playerConfirmed = new bool[maxPlayers];   // �����Ԕz���������
        controllerAssignments = new Dictionary<int, int>(); // �R���g���[���[���蓖��

        if (availableCharacters == null || availableCharacters.Count == 0)
        {
            Debug.LogError("availableCharacters �ɃL�����N�^�[�f�[�^���ݒ肳��Ă��܂���I");
            return;
        }

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
            UpdateCharacterDisplay(i);
        }

        // �v���C���[�ȊO��UI�͔�\��
        for (int i = activePlayerCount; i < maxPlayers; i++)
        {
            characterImages[i].gameObject.SetActive(false);
            characterNames[i].gameObject.SetActive(false);
        }

        // �R���g���[���[���蓖��
        AssignControllersToPlayers();
    }

    private void AssignControllersToPlayers()
    {
        string[] joystickNames = Input.GetJoystickNames();

        for (int i = 0; i < activePlayerCount; i++)
        {
            if (i < joystickNames.Length && !string.IsNullOrEmpty(joystickNames[i]))
            {
                controllerAssignments[i] = i; // �R���g���[���[���v���C���[�Ɋ��蓖��
                Debug.Log($"Controller {i + 1} assigned to Player {i + 1}");
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < activePlayerCount; i++)
        {
            if (controllerAssignments.ContainsKey(i))
            {
                int controllerIndex = controllerAssignments[i];

                // �R���g���[���[���͂̎擾
                string horizontalAxis = $"Joystick{controllerIndex + 1}_Horizontal";
                string confirmButton = $"Joystick{controllerIndex + 1}_Confirm";

                // ���E�ړ��ŃL�����N�^�[�ύX
                if (Input.GetAxis(horizontalAxis) > 0.5f)
                {
                    ShowNextCharacter(i);
                }
                else if (Input.GetAxis(horizontalAxis) < -0.5f)
                {
                    ShowPreviousCharacter(i);
                }

                // ����{�^���ŃL�����m��
                if (Input.GetButtonDown(confirmButton))
                {
                    ConfirmCharacter(i);
                }
            }
        }
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

    private void ConfirmCharacter(int playerIndex)
    {
        Character selectedCharacter = availableCharacters[currentIndices[playerIndex]];

        if (GameData.selectedCharacters.Contains(selectedCharacter))
        {
            Debug.Log("���̃v���C���[�����łɑI�����Ă���L�����N�^�[�ł��I");
            return;
        }

        GameData.selectedCharacters[playerIndex] = selectedCharacter;
        Debug.Log($"Player {playerIndex + 1} selected {selectedCharacter.characterName}");

        playerConfirmed[playerIndex] = true;

        CheckAllPlayersConfirmed();
    }

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

    private void UpdateCharacterDisplay(int playerIndex)
    {
        Character currentCharacter = availableCharacters[currentIndices[playerIndex]];
        characterImages[playerIndex].sprite = currentCharacter.image;
        characterNames[playerIndex].text = currentCharacter.characterName;
    }

    private void CheckAllPlayersConfirmed()
    {
        if (playerConfirmed.All(confirmed => confirmed))
        {
            Debug.Log("All players confirmed. Game can start.");
            startGameButton.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("NSScene");
    }
}
