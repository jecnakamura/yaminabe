using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    public Button[] playerCharacterButtons;   // �v���C���[�p�L�����N�^�[�{�^��
    public ToggleGroup[] npcStrengthToggles;  // NPC�����I��p�g�O���O���[�v�iNPC1, NPC2, NPC3...�j
    public Button startGameButton;            // �Q�[���J�n�{�^��

    private void Start()
    {
        // �Q�[���J�n�{�^���̓v���C���[�l�����I�΂ꂽ��L����
        startGameButton.interactable = false;
    }

    public void SelectPlayerCharacter(int playerIndex, Character selectedCharacter)
    {
        // �v���C���[���L�����N�^�[��I�񂾂�L������ۑ�
        GameData.selectedCharacters[playerIndex] = selectedCharacter;
        CheckIfAllPlayersSelected();
    }

    public void SelectNPCStrength(int npcIndex, int strengthIndex)
    {
        // NPC�̋�����ݒ�
        NPCStrength selectedStrength = (NPCStrength)strengthIndex;
        GameData.npcs[npcIndex].npcStrength = selectedStrength;
    }

    private void CheckIfAllPlayersSelected()
    {
        // �v���C���[�S�����L�����N�^�[��I�񂾂�Q�[���J�n�{�^����L����
        bool allPlayersSelected = true;
        foreach (var character in GameData.selectedCharacters)
        {
            if (character == null)
            {
                allPlayersSelected = false;
                break;
            }
        }

        startGameButton.interactable = allPlayersSelected;
    }
}
