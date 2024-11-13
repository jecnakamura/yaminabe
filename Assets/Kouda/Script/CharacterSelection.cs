using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters;  // 使用可能なキャラクターリスト
    private List<Character> selectedCharacters = new List<Character>();  // 選択されたキャラクターのリスト
    private int currentPlayer = 0;  // 現在キャラを選んでいるプレイヤー番号
    private int maxPlayers = 4;     // 最大プレイヤー数

    // キャラクターを選択するメソッド
    public void SelectCharacter(Character character)
    {
        if (selectedCharacters.Contains(character)) return; // キャラが選択済みの場合は何もしない
        selectedCharacters.Add(character);

        currentPlayer++;

        if (currentPlayer >= maxPlayers)
        {
            StartGame();  // 全プレイヤーが選択を終えたらゲーム開始
        }
    }

    // ゲーム開始メソッド
    private void StartGame()
    {
        // 選択したキャラクターを次のシーンで使えるように保存（プレイヤー情報の転送など）
        GameData.selectedCharacters = selectedCharacters;
        SceneManager.LoadScene("GameScene");  // ゲーム本編シーンへ遷移
    }
}
