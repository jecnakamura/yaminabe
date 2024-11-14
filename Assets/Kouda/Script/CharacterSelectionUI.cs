using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    public Button[] playerCharacterButtons;   // プレイヤー用キャラクターボタン
    public ToggleGroup[] npcStrengthToggles;  // NPC強さ選択用トグルグループ（NPC1, NPC2, NPC3...）
    public Button startGameButton;            // ゲーム開始ボタン

    private void Start()
    {
        // ゲーム開始ボタンはプレイヤー人数が選ばれたら有効化
        startGameButton.interactable = false;
    }

    public void SelectPlayerCharacter(int playerIndex, Character selectedCharacter)
    {
        // プレイヤーがキャラクターを選んだらキャラを保存
        GameData.selectedCharacters[playerIndex] = selectedCharacter;
        CheckIfAllPlayersSelected();
    }

    public void SelectNPCStrength(int npcIndex, int strengthIndex)
    {
        // NPCの強さを設定
        NPCStrength selectedStrength = (NPCStrength)strengthIndex;
        GameData.npcs[npcIndex].npcStrength = selectedStrength;
    }

    private void CheckIfAllPlayersSelected()
    {
        // プレイヤー全員がキャラクターを選んだらゲーム開始ボタンを有効化
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
