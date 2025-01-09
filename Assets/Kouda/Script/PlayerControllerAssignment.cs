using UnityEngine;

public class PlayerControllerAssignment : MonoBehaviour
{
    private int maxPlayers = 4; // �ő�v���C���[��
    private bool[] controllersConnected;
    public int[] assignedControllers; // �v���C���[���ƂɊ��蓖�Ă�ꂽ�R���g���[���[�ԍ�

    void Start()
    {
        controllersConnected = new bool[maxPlayers];
        assignedControllers = new int[maxPlayers];

        for (int i = 0; i < maxPlayers; i++)
        {
            assignedControllers[i] = -1; // �����蓖�Ă�����
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
                assignedControllers[playerIndex] = i; // �v���C���[�ɃR���g���[���[�����蓖��
                Debug.Log($"Player {playerIndex + 1} �� Controller {i + 1} �����蓖�Ă܂����B");
                playerIndex++;
            }
        }

        // ���蓖�Ă�GameData�ɕۑ�
        GameData.controllerAssignments = assignedControllers;
    }
}
