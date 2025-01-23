using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI rankingText; // ランキングを表示するUIテキスト
    public Button restartButton; // もう一度遊ぶボタン
    public Button quitButton;    // 終了ボタン

    void Start()
    {
        // GameManager からプレイヤーデータを取得
        List<Player> players = GameManager.Instance.GetPlayers();

        if (players == null || players.Count == 0)
        {
            Debug.LogError("プレイヤーデータが設定されていません");
            return;
        }

        // スコア順に並び替え（降順）
        players.Sort((p1, p2) => p2.CalculateScore().CompareTo(p1.CalculateScore()));

        // ランキングを表示
        DisplayRanking(players);

        // ボタンのイベントを設定
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void DisplayRanking(List<Player> players)
    {
        string result = "結果発表！\n";

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

            result += $"{rank}位: プレイヤー{player.ID + 1} - スコア: {currentScore:F2}\n";
            result += $"食材: {player.ingredients.Count}個\n";

            previousScore = currentScore; // 前回のスコアを更新
        }

        rankingText.text = result;
    }

    void RestartGame()
    {
        // スタートシーンに戻る
        SceneManager.LoadScene("TitleScene");
    }

    void QuitGame()
    {
        // アプリケーションを終了
        Application.Quit();
    }
}
