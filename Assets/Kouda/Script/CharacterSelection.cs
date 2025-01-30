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
    private int activePlayerIndex;
    private int maxPlayers = 4;
    private bool[] playerConfirmed;                // プレイヤーのキャラ決定状態
    private Dictionary<int, int> controllerAssignments; // コントローラー番号 -> プレイヤー番号
    private bool allNPCStrengthsSet = false;       // 全NPCの強さが決定したか

    private void Start()
    {
        activePlayerCount = GameData.playerCount;  // 保存されたプレイヤー人数を取得
        currentIndices = new int[maxPlayers];     // インデックス配列を初期化
        playerConfirmed = new bool[maxPlayers];   // 決定状態配列を初期化
        controllerAssignments = new Dictionary<int, int>(); // コントローラー割り当て

        if (availableCharacters == null || availableCharacters.Count == 0)
        {
            Debug.LogError("availableCharacters にキャラクターデータが設定されていません！");
            return;
        }

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

        // コントローラー割り当て
        AssignControllersToPlayers();
    }

    private void AssignControllersToPlayers()
    {
        string[] joystickNames = Input.GetJoystickNames();

        for (int i = 0; i < activePlayerCount; i++)
        {
            if (i < joystickNames.Length && !string.IsNullOrEmpty(joystickNames[i]))
            {
                controllerAssignments[i] = i; // コントローラーをプレイヤーに割り当て
                Debug.Log($"Controller {i + 1} assigned to Player {i + 1}");
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < activePlayerCount; i++)
        {
            if (controllerAssignments.ContainsKey(i))
            {
                int controllerIndex = controllerAssignments[i];

                // コントローラー入力の取得
                string horizontalAxis = $"Joystick{controllerIndex + 1}_Horizontal";
                string confirmButton = $"Joystick{controllerIndex + 1}_Confirm";

                // 左右移動でキャラクター変更
                if (Input.GetAxis(horizontalAxis) > 0.5f)
                {
                    ShowNextCharacter(i);
                }
                else if (Input.GetAxis(horizontalAxis) < -0.5f)
                {
                    ShowPreviousCharacter(i);
                }

                // 決定ボタンでキャラ確定
                if (Input.GetButtonDown(confirmButton))
                {
                    ConfirmCharacter(i);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("End");

            TestAllPlayer();
        }

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

    private void ConfirmCharacter(int playerIndex)
    {
        Character selectedCharacter = availableCharacters[currentIndices[playerIndex]];

        if (GameData.selectedCharacters.Contains(selectedCharacter))
        {
            Debug.Log("他のプレイヤーがすでに選択しているキャラクターです！");
            return;
        }

        GameData.selectedCharacters[playerIndex] = selectedCharacter;
        Debug.Log($"Player {playerIndex + 1} selected {selectedCharacter.characterName}");

        playerConfirmed[playerIndex] = true;
        nextButtons[playerIndex].gameObject.SetActive(false);
        prevButtons[playerIndex].gameObject.SetActive(false);
        confirmButtons[playerIndex].gameObject.SetActive(false);

        activePlayerIndex++;
        CheckAllPlayersConfirmed();
    }

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

    private void UpdateCharacterDisplay(int playerIndex)
    {
        Character currentCharacter = availableCharacters[currentIndices[playerIndex]];
        characterImages[playerIndex].sprite = currentCharacter.image;
        characterNames[playerIndex].text = currentCharacter.characterName;
    }

    private void CheckAllPlayersConfirmed()
    {
        if(activePlayerIndex == activePlayerCount)
        {
                Debug.Log("All players confirmed. Game can start.");

                startGameButton.gameObject.SetActive(true);
        }

    }

    private void TestAllPlayer()
    {
        for(int i = 0; i < maxPlayers; i++)
        {
            Character selectedCharacter = availableCharacters[currentIndices[i]];

            GameData.selectedCharacters[i] = selectedCharacter;
            Debug.Log($"Player {i + 1} selected {selectedCharacter.characterName}");

            playerConfirmed[i] = true;

            StartGame();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("NSScene");
    }
}
