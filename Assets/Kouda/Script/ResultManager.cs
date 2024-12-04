using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI rankingText; // ランキングを表示するUIテキスト
    public Button restartButton; // もう一度遊ぶボタン
    public Button quitButton;    // 終了ボタン
    /*
private List<PlayerData> players;


    void Start()
    {
        // GameManager からプレイヤーデータを取得
        rankedPlayers = GameManager.Instance.GetPlayerData();

        // ランキングを表示
        DisplayRanking(rankedPlayers);

        // ボタンのイベントを設定
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void DisplayRanking(List<Player> players)
    {
        string result = "結果発表！\n";

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            result += $"{i + 1}位: プレイヤー{player.ID} - スコア: {player.CalculateScore():F2}\n";
            result += $"食材: {player.ingredients.Count}個\n";
        }

        rankingText.text = result;
    }*/
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
