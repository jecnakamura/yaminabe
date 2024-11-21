using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    public Text rankingText; // ランキングを表示するUIテキスト
    public Button restartButton; // もう一度遊ぶボタン
    public Button quitButton;    // 終了ボタン
    /*
private List<PlayerData> players;

void Start()
{
    // 前のシーンからプレイヤーデータを取得
    players = GameManager.Instance.GetPlayerData();

    // ランキングを計算して表示
    DisplayRanking(players);

    // ボタンのイベントを設定
    restartButton.onClick.AddListener(RestartGame);
    quitButton.onClick.AddListener(QuitGame);
}

void DisplayRanking(List<PlayerData> players)
{
    string result = "結果発表！\n";

    for (int i = 0; i < players.Count; i++)
    {
        var player = players[i];
        result += $"{i + 1}位: {player.name} - 食材: {player.ownedIngredients.Count}個";
        if (player.hasKey)
            result += "（鍵あり）";
        result += "\n";
    }

    rankingText.text = result;
}
*/
    void RestartGame()
    {
        // スタートシーンに戻る
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }

    void QuitGame()
    {
        // アプリケーションを終了
        Application.Quit();
    }
}
