using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public List<TextMeshProUGUI> rankingTexts; // プレイヤー毎の順位テキスト表示用リスト
    public Button restartButton; // もう一度遊ぶボタン
    public Button quitButton;    // 終了ボタン

    public List<Image> potImage;  // 鍋のイラスト用

    public Sprite meatPotSprite; // 肉の鍋イラスト
    public Sprite fishPotSprite; // 魚の鍋イラスト
    public Sprite vegetablePotSprite; // 野菜の鍋イラスト
    public Sprite defaultPotSprite; // デフォルトの鍋イラスト

    void Start()
    {
        // GameManager からプレイヤーデータを取得
        List<Player> players = GameManager.Instance.GetPlayers();

        if (players == null || players.Count == 0)
        {
            Debug.LogError("プレイヤーデータが設定されていません");
            return;
        }

        int maxPlayers = 4;
        int activePlayerCount = GameData.playerCount;

        // プレイヤー以外のUIは非表示
        for (int i = activePlayerCount; i < maxPlayers; i++)
        {
            potImage[i].gameObject.SetActive(false);
            rankingTexts[i].gameObject.SetActive(false);
        }

        // スコア順に並び替え（降順）
        players.Sort((p1, p2) => p2.CalculateScore().CompareTo(p1.CalculateScore()));

        // ランキングを表示
        DisplayRanking(players);

        // ボタンのイベントを設定
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);

        // 鍋の画像を設定
        UpdatePotImages(players);
    }

    public void DisplayRanking(List<Player> players)
    {
        int rank = 1; // 順位の初期値
        float previousScore = -1; // 前のスコア（同点処理用）

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            float currentScore = player.CalculateScore();

            // 同点の場合は順位を変更せず、前の順位を保持
            if (currentScore != previousScore)
            {
                rank = i + 1; // スコアが変わったら順位を更新
            }

            // プレイヤーの順位とスコアを対応するテキストに設定
            if (i < rankingTexts.Count)
            {
                rankingTexts[i].text = $"{rank}位: プレイヤー{player.ID + 1} - スコア: {currentScore:F2}";
            }

            previousScore = currentScore; // 前回のスコアを更新
        }
    }

    void UpdatePotImages(List<Player> players)
    {
        // 各プレイヤーごとに鍋の画像を設定
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            string mostFrequentRoulette = player.GetMostFrequentRoulette();

            // 最も多く回されたルーレットに対応する鍋を選択
            Sprite selectedPotSprite = defaultPotSprite; // デフォルトの鍋に設定

            switch (mostFrequentRoulette)
            {
                case "Meat":
                    selectedPotSprite = meatPotSprite;
                    break;
                case "Fish":
                    selectedPotSprite = fishPotSprite;
                    break;
                case "Vegetable":
                    selectedPotSprite = vegetablePotSprite;
                    break;
                default:
                    selectedPotSprite = defaultPotSprite;
                    break;
            }

            // プレイヤーの順位に基づいて鍋を設定 (1位 -> potImage[0], 2位 -> potImage[1] 等)
            if (i < potImage.Count)
            {
                potImage[i].sprite = selectedPotSprite;
            }
        }
    }

    void RestartGame()
    {
        // スタートシーンに戻る
        SceneManager.LoadScene("TiteScens");
    }

    void QuitGame()
    {
        // アプリケーションを終了
        Application.Quit();
    }
}
