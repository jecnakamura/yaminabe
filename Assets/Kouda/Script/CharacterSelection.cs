using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters;  // �g�p�\�ȃL�����N�^�[���X�g
    private List<Character> selectedCharacters = new List<Character>();  // �I�����ꂽ�L�����N�^�[�̃��X�g
    public List<NPC> npcs;  // NPC�̃��X�g
    private int currentPlayer = 0;  // ���݃L������I��ł���v���C���[�ԍ�
    private int maxPlayers = 4;     // �ő�v���C���[��

    // �L�����N�^�[��I�����郁�\�b�h
    public void SelectCharacter(Character character)
    {
        if (selectedCharacters.Contains(character)) return; // �L�������I���ς݂̏ꍇ�͉������Ȃ�
        selectedCharacters.Add(character);

        currentPlayer++;

        if (currentPlayer >= maxPlayers)
        {
            StartGame();  // �S�v���C���[���I�����I������Q�[���J�n
        }
    }
    public void SetNPCStrength(int npcIndex, NPCStrength strength)
    {
        npcs[npcIndex].npcStrength = strength;
    }

    public void AssignNPCStrength()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            // NPC�̋����������_���ɐݒ�i��: Weak, Normal, Strong�j
            NPCStrength randomStrength = (NPCStrength)Random.Range(0, 3);
            npcs[i].npcStrength = randomStrength;
        }
    }
    // �Q�[���J�n���\�b�h
    private void StartGame()
    {
        // �I�������L�����N�^�[�����̃V�[���Ŏg����悤�ɕۑ��i�v���C���[���̓]���Ȃǁj
        GameData.selectedCharacters = selectedCharacters;
        SceneManager.LoadScene("GameScene");  // �Q�[���{�҃V�[���֑J��
    }
}
