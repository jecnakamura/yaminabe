using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> availableCharacters;    // 全キャラクターリスト
    public List<Image> characterImages;            // 各プレイヤーのキャラクター画像
    public List<TextMeshProUGUI> characterNames;   // 各プレイヤーのキャラクター名
    public List<Button> nextButtons;               // 「＞」ボタン
    public List<Button> prevButtons;               // 「＜」ボタン
    public List<Button> confirmButtons;            // 決定ボタン
    public List<GameObject> npcStrengthSelectors;  // NPCの強さ選択オブジェクト
    public Button startGameButton;                 // ゲームスタートボタン
    private int[] currentIndices;                  // 各プレイヤーの現在インデックス
    private int activePlayerCount;                 // 有効なプレイヤー数
    private int maxPlayers = 4;
    private bool allPlayersConfirmed = false;      // プレイヤー全員がキャラを決定したか
    private bool allNPCStrengthsSet = false;       // 全NPCの強さが決定したか

    private void Start()
    {
        activePlayerCount = GameData.playerCount;  // 保存されたプレイヤー人数を取得
        currentIndices = new int[maxPlayers];     // インデックス配列を初期化

        if (availableCharacters == null || availableCharacters.Count == 0)
        {
            Debug.LogError("availableCharacters にキャラクターデータが設定されていません！");
            return;
        }

        // プレイヤーとNPCのUIを設定
        SetupPlayerUI();

        // NPC用UIとゲームスタートボタンは非表示
        foreach (var npcSelector in npcStrengthSelectors)
            npcSelector.SetActive(false);

        startGameButton.gameObject.SetActive(false);
    }

    private void SetupPlayerUI()
    {
        // プレイヤーUIを初期化
        for (int i = 0; i < activePlayerCount; i++)
        {
            currentIndices[i] = i % availableCharacters.Count;

            characterImages[i].gameObject.SetActive(true);
            characterNames[i].gameObject.SetActive(true);
            nextButtons[i].gameObject.SetActive(true);
            prevButtons[i].gameObject.SetActive(true);
            confirmButtons[i].gameObject.SetActive(true);
            currentIndices[i] = currentIndices[i] % availableCharacters.Count;
            UpdateCharacterDisplay(i);
        }

        // プレイヤー以外のUIは非表示
        for (int i = activePlayerCount; i < maxPlayers; i++)
        {
            characterImages[i].gameObject.SetActive(false);
            characterNames[i].gameObject.SetActive(false);
            nextButtons[i].gameObject.SetActive(false);
            prevButtons[i].gameObject.SetActive(false);
            confirmButtons[i].gameObject.SetActive(false);
        }
    }

    private void CheckAllPlayersConfirmed()
    {
        allPlayersConfirmed = true;

        for (int i = 0; i < activePlayerCount; i++)
        {
            if (GameData.selectedCharacters[i] == null)
            {
                allPlayersConfirmed = false;
                break;
            }
        }

        if (allPlayersConfirmed)
        {
            Debug.Log("All players confirmed their characters.");
            // プレイヤーが4人ならゲームスタートボタンを表示する
            if (activePlayerCount == maxPlayers)
            {
                startGameButton.gameObject.SetActive(true);
            }
            if (activePlayerCount < maxPlayers)
            {
                AssignNPCCharacters();
            }
        }
    }

    private void AssignNPCCharacters()
    {
        // プレイヤーが選択したキャラクターを除外
        List<Character> unselectedCharacters = new List<Character>(availableCharacters);
        foreach (var character in GameData.selectedCharacters)
        {
            if (character != null)
                unselectedCharacters.Remove(character);
        }

        // NPCキャラクターを割り当て
        GameData.npcData.Clear();
        for (int i = 0; i < maxPlayers - activePlayerCount; i++)
        {
            NPCData npc = new NPCData
            {
                assignedCharacter = unselectedCharacters[i],
                npcStrength = NPCStrength.Unset
            };
            GameData.npcData.Add(npc);
            npcStrengthSelectors[i].SetActive(true); // NPCの強さ選択UIを表示
        }
    }

    // NPCの強さを設定するメソッド
    public void SetNPCStrength(int npcIndex, NPCStrength strength)
    {
        if (npcIndex < GameData.npcData.Count)
        {
            GameData.npcData[npcIndex].npcStrength = strength;
            Debug.Log($"NPC {npcIndex} strength set to {strength}");

            // NPCの強さが全て設定されているか確認
            CheckAllNPCStrengthsSet();
        }
    }

    private void CheckAllNPCStrengthsSet()
    {
        allNPCStrengthsSet = true;

        foreach (var npc in GameData.npcData)
        {
            if (npc.npcStrength == NPCStrength.Unset)
            {
                allNPCStrengthsSet = false;
                break;
            }
        }

        if (allNPCStrengthsSet)
        {
            Debug.Log("All NPC strengths set. Game can start.");
            startGameButton.gameObject.SetActive(true); // ゲームスタートボタンを表示
        }
    }

    // NPCの強さ選択ボタン（Weak, Normal, Strong）の処理
    public void SetWeak(int npcIndex)
    {
        SetNPCStrength(npcIndex, NPCStrength.Weak);
    }

    public void SetNormal(int npcIndex)
    {
        SetNPCStrength(npcIndex, NPCStrength.Normal);
    }

    public void SetStrong(int npcIndex)
    {
        SetNPCStrength(npcIndex, NPCStrength.Strong);
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

    // 他のプレイヤーが選択したキャラクターかどうかを確認
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

    // ConfirmCharacter メソッドの修正
    public void ConfirmCharacter(int playerIndex)
    {
        Character selectedCharacter = availableCharacters[currentIndices[playerIndex]];

        // 他プレイヤーが選択しているキャラクターを再確認
        if (GameData.selectedCharacters.Contains(selectedCharacter))
        {
            Debug.Log("他のプレイヤーがすでに選択しているキャラクターです！");
            return;
        }

        GameData.selectedCharacters[playerIndex] = selectedCharacter;
        Debug.Log($"Player {playerIndex + 1} selected {selectedCharacter.characterName}");

        // 決定ボタンを無効化
        confirmButtons[playerIndex].interactable = false;

        // 全プレイヤーがキャラを決定したか確認
        CheckAllPlayersConfirmed();
    }

    private void UpdateCharacterDisplay(int playerIndex)
    {
        Character currentCharacter = availableCharacters[currentIndices[playerIndex]];
        characterImages[playerIndex].sprite = currentCharacter.image;
        characterNames[playerIndex].text = currentCharacter.characterName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("NSScene");
    }
}
