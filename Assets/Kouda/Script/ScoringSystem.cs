using System.Collections.Generic;
using System.Linq;

public class ScoringSystem
{
    public List<Player> players;

    public ScoringSystem(List<Player> players)
    {
        this.players = players;
    }

    // �e�v���C���[�̍ŏI�X�R�A���v�Z���A���ʂ����肷�郁�\�b�h
    public List<Player> CalculateRanking()
    {
        // �v���C���[���Ƃ̃X�R�A���v�Z���A�X�R�A�̍������ɕ��ёւ���
        return players.OrderByDescending(player => player.CalculateScore()).ToList();
    }
}
