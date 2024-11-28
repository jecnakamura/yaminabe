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

        Debug.Log($"�v���C���[ {playerIndex + 1} �̃X�R�A: {playerScores[playerIndex]}");
    }

    public int GetScore(int playerIndex)
    {
        return playerScores.ContainsKey(playerIndex) ? playerScores[playerIndex] : 0;
    }

    public void DisplayFinalScores()
    {
        Debug.Log("�ŏI�X�R�A: ");
        foreach (var score in playerScores)
        {
            Debug.Log($"�v���C���[ {score.Key + 1}: {score.Value}");
        }
    }
}
