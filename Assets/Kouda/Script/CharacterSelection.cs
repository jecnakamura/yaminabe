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
    public List<GameObject> npcStrengthSelectors; // NPC強さ選択用のオブジェクト（プレイヤーがいない場合に表示）

    private int[] currentIndices;                // プレイヤーごとの現在インデックス
    private int maxPlayers = 4;
    private int activePlayerCount;

    //test
    public Sprite characterImage1;
    public Sprite characterImage2;
    public Sprite characterImage3;
    public Sprite characterImage4;
    private void Start()
    {
        // GameDataからプレイヤー人数を取得
        activePlayerCount = GameData.playerCount;

        if (activePlayerCount <= 0 || activePlayerCount > maxPlayers)
        {
            Debug.LogError("不正なプレイヤー人数です。デフォルト値を使用します。");
            activePlayerCount = maxPlayers; // デフォルト値にフォールバック
        }
        // プレイヤー数分のインデックスを初期化
        currentIndices = new int[maxPlayers];
        // サンプルキャラクターを作成して追加
        availableCharacters = new List<Character>
    {
        new Character { name = "Character1", image = characterImage1 },
        new Character { name = "Character2", image = characterImage2 },
        new Character { name = "Character3", image = characterImage3 },
        new Character { name = "Character4", image = characterImage4 }
    };
        // 初期表示を設定
        for (int i = 0; i < maxPlayers; i++)
        {
            if (i < activePlayerCount)
            {
                // アクティブなプレイヤーの場合、キャラクターを割り当てる
                currentIndices[i] = i % availableCharacters.Count;
                characterImages[i].gameObject.SetActive(true);
                characterNames[i].gameObject.SetActive(true);
                nextButtons[i].gameObject.SetActive(true);
                prevButtons[i].gameObject.SetActive(true);
                confirmButtons[i].gameObject.SetActive(true);
                npcStrengthSelectors[i].SetActive(false); // NPC選択UIは非表示
            }
            else
            {
                // プレイヤーがいない場合、NPC強さ選択を表示
                characterImages[i].gameObject.SetActive(false);
                characterNames[i].gameObject.SetActive(false);
                nextButtons[i].gameObject.SetActive(false);
                prevButtons[i].gameObject.SetActive(false);
                confirmButtons[i].gameObject.SetActive(false);
                npcStrengthSelectors[i].SetActive(true);
            }
        }
        // キャラクター表示を更新
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
        if (availableCharacters == null || availableCharacters.Count == 0)
        {
            Debug.LogError("キャラクターリストが空です。キャラクターを設定してください。");
            return; // エラー回避
        }

        for (int i = 0; i < maxPlayers; i++)
        {
            // インデックスがリストの範囲内かを確認
            if (currentIndices[i] >= 0 && currentIndices[i] < availableCharacters.Count)
            {
                Character currentCharacter = availableCharacters[currentIndices[i]];
                characterImages[i].sprite = currentCharacter.image;
                characterNames[i].text = currentCharacter.name;
            }
            else
            {
                Debug.LogWarning($"Player {i + 1} のインデックスが範囲外です。");
            }
        }
    }
}
