using UnityEngine;

public class PlayerControllerAssignment : MonoBehaviour
{
    // �v���C���[���̍ő�l
    private int maxPlayers = 4;

    // �R���g���[���[�̐ڑ��󋵂�ێ����郊�X�g
    private bool[] controllersConnected;

    void Start()
    {
        // �R���g���[���[�̐ڑ��󋵂��Ǘ�����z���������
        controllersConnected = new bool[maxPlayers];

        // �ڑ�����Ă���R���g���[���[�̐����擾
        CheckControllers();
    }

    void Update()
    {
        // �R���g���[���[���ڑ����ꂽ�ꍇ�A�ڑ��̕ύX�����m
        CheckControllers();
    }

    void CheckControllers()
    {
        // �ڑ�����Ă���R���g���[���[�̐������o
        string[] joystickNames = Input.GetJoystickNames();

        // ���ׂẴR���g���[���[���m�F
        for (int i = 0; i < maxPlayers; i++)
        {
            // �R���g���[���[���ڑ�����Ă���ꍇ
            if (i < joystickNames.Length && !string.IsNullOrEmpty(joystickNames[i]))
            {
                if (!controllersConnected[i])
                {
                    // �V���ɃR���g���[���[���ڑ����ꂽ�ꍇ�A�v���C���[�ԍ������蓖��
                    controllersConnected[i] = true;
                    Debug.Log($"�R���g���[���[{i + 1}���ڑ�����܂����B�v���C���[{i + 1}�Ɋ��蓖�Ă܂����B");
                }
            }
            else
            {
                // �R���g���[���[���ؒf���ꂽ�ꍇ�A�ڑ���Ԃ����Z�b�g
                if (controllersConnected[i])
                {
                    controllersConnected[i] = false;
                    Debug.Log($"�R���g���[���[{i + 1}���ؒf����܂����B");
                }
            }
        }
    }
}
