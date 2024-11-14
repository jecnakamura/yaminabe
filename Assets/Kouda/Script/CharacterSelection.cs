using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters; // 使用可能なキャラクターリスト
    private List<Character> selectedCharacters = new List<Character>(); // 選択されたキャラクターのリスト
    public List<NPC> npcs; // NPCのリスト
    private int currentPlayer = 0; // 現在キャラを選んでいるプレイヤー番号
    private int maxPlayers = 4;    // 最大プレイヤー数

    // キャラクターを選択するメソッド
    public void SelectCharacter(Character character)
    {
        if (selectedCharacters.Contains(character)) return; // キャラが選択済みの場合は何もしない
        selectedCharacters.Add(character);

        currentPlayer++;

        if (currentPlayer >= maxPlayers)
        {
            StartGame(); // 全プレイヤーが選択を終えたらゲーム開始
        }
    }

    // NPCの強さを設定するメソッド（UIから呼び出し）
    public void SetNPCStrength(int npcIndex, NPCStrength strength)
    {
        if (npcIndex < npcs.Count)
        {
            npcs[npcIndex].npcStrength = strength;
            Debug.Log($"NPC {npcIndex + 1} の強さが {strength} に設定されました");
        }
    }

    // ゲーム開始メソッド
    private void StartGame()
    {
        // 選択したキャラクターとNPC情報を次のシーンで使えるように保存
        GameData.selectedCharacters = selectedCharacters;
        GameData.npcs = npcs;

        SceneManager.LoadScene("GameScene"); // ゲーム本編シーンへ遷移
    }
}
