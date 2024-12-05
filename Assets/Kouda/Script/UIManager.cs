using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI turnInfoText;

    // �^�[������\��
    public void ShowTurnInfo(Player player)
    {
        turnInfoText.text = $"{player.ID} �̃^�[���ł��I";
    }

    // �Q�[���I�����UI�X�V
    public void ShowEndGameInfo()
    {
        turnInfoText.text = "�Q�[���I���I";
    }
}
