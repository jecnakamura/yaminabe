using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI turnInfoText;

    // ターン情報を表示
    public void ShowTurnInfo(Player player)
    {
        turnInfoText.text = $"{player.ID} のターンです！";
    }

    // ゲーム終了後のUI更新
    public void ShowEndGameInfo()
    {
        turnInfoText.text = "ゲーム終了！";
    }
}
