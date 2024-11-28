using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Dictionary<int, int> playerScores = new Dictionary<int, int>();

    public void UpdateScore(int playerIndex, int scoreChange)
    {
        if (!playerScores.ContainsKey(playerIndex))
        {
            playerScores[playerIndex] = 0;
        }

        playerScores[playerIndex] += scoreChange;
        if (playerScores[playerIndex] < 0) playerScores[playerIndex] = 0;

        Debug.Log($"プレイヤー {playerIndex + 1} のスコア: {playerScores[playerIndex]}");
    }

    public int GetScore(int playerIndex)
    {
        return playerScores.ContainsKey(playerIndex) ? playerScores[playerIndex] : 0;
    }

    public void DisplayFinalScores()
    {
        Debug.Log("最終スコア: ");
        foreach (var score in playerScores)
        {
            Debug.Log($"プレイヤー {score.Key + 1}: {score.Value}");
        }
    }
}
