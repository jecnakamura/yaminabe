using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters;  // 全キャラクターリスト
    public List<Image> characterImages;          // 各プレイヤーのキャラクター画像
    public List<TextMeshProUGUI> characterNames; // 各プレイヤーのキャラクター名 (TextMeshProに変更)
    public List<Button> nextButtons;             // 「＞」ボタン
    public List<Button> prevButtons;             // 「＜」ボタン
    public List<Button> confirmButtons;          // 決定ボタン
    public List<Button> npcStrengthButtons;      // NPC強さボタン

    private int[] currentIndices;                // プレイヤーごとの現在インデックス
    private int maxPlayers = 4;

    private void Start()
    {
        currentIndices = new int[maxPlayers];
        UpdateCharacterDisplay();
    }

    // 「＞」ボタンが押されたとき
    public void ShowNextCharacter(int playerIndex)
    {
        currentIndices[playerIndex] = (currentIndices[playerIndex] + 1) % availableCharacters.Count;
        UpdateCharacterDisplay();
    }

    // 「＜」ボタンが押されたとき
    public void ShowPreviousCharacter(int playerIndex)
    {
        currentIndices[playerIndex] = (currentIndices[playerIndex] - 1 + availableCharacters.Count) % availableCharacters.Count;
        UpdateCharacterDisplay();
    }

    // キャラクターを確定
    public void ConfirmCharacter(int playerIndex)
    {
        Character selected = availableCharacters[currentIndices[playerIndex]];
        Debug.Log($"Player {playerIndex + 1} has selected {selected.name}");
        // 確定キャラクターの設定処理を追加
    }

    // NPCの強さを設定
    public void SetNPCStrength(int npcIndex, NPCStrength strength)
    {
        // NPCの強さを設定する処理
        Debug.Log($"NPC {npcIndex + 1} の強さが {strength} に設定されました");
    }

    private void UpdateCharacterDisplay()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            Character currentCharacter = availableCharacters[currentIndices[i]];
            characterImages[i].sprite = currentCharacter.image;
            characterNames[i].text = currentCharacter.name;  // TextMeshProにキャラクター名を表示
        }
    }
}
