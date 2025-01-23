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

        // ランキングを表示
        DisplayRanking(players);

        // ボタンのイベントを設定
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void DisplayRanking(List<Player> players)
    {
        string result = "結果発表！\n";

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            result += $"{i + 1}位: プレイヤー{player.ID} - スコア: {player.CalculateScore():F2}\n";
            result += $"食材: {player.ingredients.Count}個\n";
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
