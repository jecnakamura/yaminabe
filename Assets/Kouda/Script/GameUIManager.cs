using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text turnInfoText;
    public Button rouletteButton;
    public Button inventoryButton;
    public Button mapButton;

    public void ShowTurnInfo(Player player)
    {
        turnInfoText.text = $"プレイヤー {player.ID} のターンです";
    }

    public void EnableButtons(bool enable)
    {
        rouletteButton.interactable = enable;
        inventoryButton.interactable = enable;
        mapButton.interactable = enable;
    }
}
