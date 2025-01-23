using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // �v���C���[���X�g��ێ�
    public List<Player> players;

    private void Awake()
    {
        // �V�[�����؂�ւ���Ă����̃I�u�W�F�N�g��j�����Ȃ��悤�ɂ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���̃I�u�W�F�N�g�̓V�[���J�ڌ���c��
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �v���C���[�f�[�^���Z�b�g���郁�\�b�h
    public void SetPlayers(List<Player> playersList)
    {
        players = playersList;
    }

    // �v���C���[�f�[�^���擾���郁�\�b�h
    public List<Player> GetPlayers()
    {
        return players;
    }
}
