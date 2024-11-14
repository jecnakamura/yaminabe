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
    public List<Button> npcStrengthButtons;      // NPC�����{�^��

    private int[] currentIndices;                // �v���C���[���Ƃ̌��݃C���f�b�N�X
    private int maxPlayers = 4;

    private void Start()
    {
        currentIndices = new int[maxPlayers];
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
        for (int i = 0; i < maxPlayers; i++)
        {
            Character currentCharacter = availableCharacters[currentIndices[i]];
            characterImages[i].sprite = currentCharacter.image;
            characterNames[i].text = currentCharacter.name;  // TextMeshPro�ɃL�����N�^�[����\��
        }
    }
}
