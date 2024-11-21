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
                Debug.Log("�C�x���g�Ȃ�");
                break;
        }
    }

    private void AddRandomIngredient(PlayerData player)
    {
        // �����_���ȐH�ނ�ǉ��i���̎����j
        string[] randomIngredients = { "����", "�ؓ�", "���炽��", "�ɂ񂶂�", "�L���x�c" };
        string randomIngredient = randomIngredients[Random.Range(0, randomIngredients.Length)];
        player.ownedIngredients.Add(new Ingredient { name = randomIngredient, genre = "�����_��" });
        Debug.Log($"{player.name}��{randomIngredient}����ɓ��ꂽ�I");
    }

    private void RemoveRandomIngredient(PlayerData player)
    {
        if (player.ownedIngredients.Count > 0)
        {
            int index = Random.Range(0, player.ownedIngredients.Count);
            Ingredient removedIngredient = player.ownedIngredients[index];
            player.ownedIngredients.RemoveAt(index);
            Debug.Log($"{player.name}��{removedIngredient.name}���������I");
        }
        else
        {
            Debug.Log($"{player.name}�͐H�ނ������Ă��Ȃ��̂ō폜�ł��܂���B");
        }
    }

    private void SwapPlayerPositions(PlayerData currentPlayer, List<PlayerData> allPlayers)
    {
        if (allPlayers.Count < 2) return;

        int randomIndex = Random.Range(0, allPlayers.Count);
        PlayerData targetPlayer = allPlayers[randomIndex];
        if (targetPlayer == currentPlayer)
        {
            Debug.Log("�����Ƃ̌����͖����ł��B");
            return;
        }

        Vector3 tempPosition = currentPlayer.transform.position;
        currentPlayer.transform.position = targetPlayer.transform.position;
        targetPlayer.transform.position = tempPosition;

        Debug.Log($"{currentPlayer.name}��{targetPlayer.name}�̈ʒu������ւ�����I");
    }

    private void GrantKey(PlayerData player)
    {
        player.hasKey = true;
        Debug.Log($"{player.name}��������ɓ��ꂽ�I");
    }

    private void StartMiniGame()
    {
        Debug.Log("�~�j�Q�[���V�[���ɑJ�ځI");
        // �V�[���؂�ւ�����������
        // SceneManager.LoadScene("MiniGameScene");
    }

    private void TriggerGoal(PlayerData player)
    {
        Debug.Log($"{player.name}���S�[�����܂����I");
        // �Q�[���I���⌋�ʉ�ʂ̏����������ɒǉ�
    }
    public List<PlayerData> CalculateResults(List<PlayerData> players)
    {
        return players.OrderByDescending(player => player.ownedIngredients.Count) // �H�ނ̐��ŏ��ʕt��
                      .ThenByDescending(player => player.hasKey ? 1 : 0)           // ���������Ă��邩�ŏ��ʂ𒲐�
                      .ToList();
    }
    */
}
