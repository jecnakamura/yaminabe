using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class MassManager : MonoBehaviour
{
    /*
    public void ExecuteMassEvent(MassEventType eventType, PlayerData currentPlayer, List<PlayerData> allPlayers)
    {
        switch (eventType)
        {
            case MassEventType.AddIngredient:
                AddRandomIngredient(currentPlayer);
                break;

            case MassEventType.RemoveIngredient:
                RemoveRandomIngredient(currentPlayer);
                break;

            case MassEventType.SwapPositions:
                SwapPlayerPositions(currentPlayer, allPlayers);
                break;

            case MassEventType.GrantKey:
                GrantKey(currentPlayer);
                break;

            case MassEventType.MiniGame:
                StartMiniGame();
                break;

            case MassEventType.Goal:
                TriggerGoal(currentPlayer);
                break;

            default:
                Debug.Log("イベントなし");
                break;
        }
    }

    private void AddRandomIngredient(PlayerData player)
    {
        // ランダムな食材を追加（仮の実装）
        string[] randomIngredients = { "白菜", "豚肉", "しらたき", "にんじん", "キャベツ" };
        string randomIngredient = randomIngredients[Random.Range(0, randomIngredients.Length)];
        player.ownedIngredients.Add(new Ingredient { name = randomIngredient, genre = "ランダム" });
        Debug.Log($"{player.name}が{randomIngredient}を手に入れた！");
    }

    private void RemoveRandomIngredient(PlayerData player)
    {
        if (player.ownedIngredients.Count > 0)
        {
            int index = Random.Range(0, player.ownedIngredients.Count);
            Ingredient removedIngredient = player.ownedIngredients[index];
            player.ownedIngredients.RemoveAt(index);
            Debug.Log($"{player.name}が{removedIngredient.name}を失った！");
        }
        else
        {
            Debug.Log($"{player.name}は食材を持っていないので削除できません。");
        }
    }

    private void SwapPlayerPositions(PlayerData currentPlayer, List<PlayerData> allPlayers)
    {
        if (allPlayers.Count < 2) return;

        int randomIndex = Random.Range(0, allPlayers.Count);
        PlayerData targetPlayer = allPlayers[randomIndex];
        if (targetPlayer == currentPlayer)
        {
            Debug.Log("自分との交換は無効です。");
            return;
        }

        Vector3 tempPosition = currentPlayer.transform.position;
        currentPlayer.transform.position = targetPlayer.transform.position;
        targetPlayer.transform.position = tempPosition;

        Debug.Log($"{currentPlayer.name}と{targetPlayer.name}の位置が入れ替わった！");
    }

    private void GrantKey(PlayerData player)
    {
        player.hasKey = true;
        Debug.Log($"{player.name}が鍵を手に入れた！");
    }

    private void StartMiniGame()
    {
        Debug.Log("ミニゲームシーンに遷移！");
        // シーン切り替え処理を実装
        // SceneManager.LoadScene("MiniGameScene");
    }

    private void TriggerGoal(PlayerData player)
    {
        Debug.Log($"{player.name}がゴールしました！");
        // ゲーム終了や結果画面の処理をここに追加
    }
    public List<PlayerData> CalculateResults(List<PlayerData> players)
    {
        return players.OrderByDescending(player => player.ownedIngredients.Count) // 食材の数で順位付け
                      .ThenByDescending(player => player.hasKey ? 1 : 0)           // 鍵を持っているかで順位を調整
                      .ToList();
    }
    */
}
