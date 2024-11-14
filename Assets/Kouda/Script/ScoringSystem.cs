using System.Collections.Generic;
using System.Linq;

public class ScoringSystem
{
    public List<Player> players;

    public ScoringSystem(List<Player> players)
    {
        this.players = players;
    }

    // 各プレイヤーの最終スコアを計算し、順位を決定するメソッド
    public List<Player> CalculateRanking()
    {
        // プレイヤーごとのスコアを計算し、スコアの高い順に並び替える
        return players.OrderByDescending(player => player.CalculateScore()).ToList();
    }
}
