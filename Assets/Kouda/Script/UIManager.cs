using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text turnInfoText;

    // �^�[������\��
    public void ShowTurnInfo(Player player)
    {
        //turnInfoText.text = $"{player.playerName} �̃^�[���ł��I";
    }

    // �Q�[���I�����UI�X�V
    public void ShowEndGameInfo()
    {
        turnInfoText.text = "�Q�[���I���I";
    }
}
