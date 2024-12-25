using UnityEngine;

public class PlayerControllerAssignment : MonoBehaviour
{
    private int maxPlayers = 4; // 最大プレイヤー数
    private bool[] controllersConnected;
    public int[] assignedControllers; // プレイヤーごとに割り当てられたコントローラー番号

    void Start()
    {
        controllersConnected = new bool[maxPlayers];
        assignedControllers = new int[maxPlayers];

        for (int i = 0; i < maxPlayers; i++)
        {
            assignedControllers[i] = -1; // 未割り当てを示す
        }

        AssignControllersToPlayers();
    }

    void AssignControllersToPlayers()
    {
        string[] joystickNames = Input.GetJoystickNames();
        int playerIndex = 0;

        for (int i = 0; i < joystickNames.Length; i++)
        {
            if (!string.IsNullOrEmpty(joystickNames[i]) && playerIndex < maxPlayers)
            {
                assignedControllers[playerIndex] = i; // プレイヤーにコントローラーを割り当て
                Debug.Log($"Player {playerIndex + 1} に Controller {i + 1} を割り当てました。");
                playerIndex++;
            }
        }

        // 割り当てをGameDataに保存
        GameData.controllerAssignments = assignedControllers;
    }
}
