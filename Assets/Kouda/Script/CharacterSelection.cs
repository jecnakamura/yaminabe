using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters; // �g�p�\�ȃL�����N�^�[���X�g
    private List<Character> selectedCharacters = new List<Character>(); // �I�����ꂽ�L�����N�^�[�̃��X�g
    public List<NPC> npcs; // NPC�̃��X�g
    private int currentPlayer = 0; // ���݃L������I��ł���v���C���[�ԍ�
    private int maxPlayers = 4;    // �ő�v���C���[��

    // �L�����N�^�[��I�����郁�\�b�h
    public void SelectCharacter(Character character)
    {
        if (selectedCharacters.Contains(character)) return; // �L�������I���ς݂̏ꍇ�͉������Ȃ�
        selectedCharacters.Add(character);

        currentPlayer++;

        if (currentPlayer >= maxPlayers)
        {
            StartGame(); // �S�v���C���[���I�����I������Q�[���J�n
        }
    }

    // NPC�̋�����ݒ肷�郁�\�b�h�iUI����Ăяo���j
    public void SetNPCStrength(int npcIndex, NPCStrength strength)
    {
        if (npcIndex < npcs.Count)
        {
            npcs[npcIndex].npcStrength = strength;
            Debug.Log($"NPC {npcIndex + 1} �̋����� {strength} �ɐݒ肳��܂���");
        }
    }

    // �Q�[���J�n���\�b�h
    private void StartGame()
    {
        // �I�������L�����N�^�[��NPC�������̃V�[���Ŏg����悤�ɕۑ�
        GameData.selectedCharacters = selectedCharacters;
        GameData.npcs = npcs;

        SceneManager.LoadScene("GameScene"); // �Q�[���{�҃V�[���֑J��
    }
}
