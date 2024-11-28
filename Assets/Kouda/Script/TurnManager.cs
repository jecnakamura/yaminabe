using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public List<Player> players; // �v���C���[�iNPC�܂ށj���X�g
    private int currentPlayerIndex = 0;
    //public MapManager mapManager; // �}�b�v�Ǘ��N���X
    //public GameUIManager uiManager; // UI�Ǘ��N���X

    private bool isGameFinished = false;

    [SerializeField] GameObject Pl;
    Vector3 spawnPosition;
    void Start()
    {
        spawnPosition = new Vector3(-23, 0, 0);
        for (int i = 0; i < GameData.playerCount; i++)
        {
            Player newPlayer = new Player
            {
                ID = i,
                chara = GameData.selectedCharacters[i],
                ingredients = new List<Ingredient>
                {
                    new Ingredient("", "", 0, 0.0f),
                }
            };
            players.Add(newPlayer);
            Instantiate(Pl, spawnPosition, Quaternion.identity);
        }
        StartCoroutine(TurnCycle());
    }

    private IEnumerator TurnCycle()
    {
        while (!isGameFinished)
        {
            Player currentPlayer = players[currentPlayerIndex];
            yield return StartCoroutine(HandlePlayerTurn(currentPlayer));

            if (CheckAllPlayersFinished())
            {
                EndGame();
                yield break;
            }

            NextPlayer();
        }
    }

    private IEnumerator HandlePlayerTurn(Player player)
    {
        // UI�Ń^�[������\��
        //uiManager.ShowTurnInfo(player);

        // ���[���b�g�V�[�����J���A���ʂ��擾

        yield return OpenRulletoButton();

        // �}�X�̈ړ�����
        //mapManager.MovePlayer(player);

        // �~�܂����}�X�̃C�x���g����
        //yield return StartCoroutine(mapManager.HandleTileEvent(player, players));
    }

    private bool CheckAllPlayersFinished()
    {
        return players.TrueForAll(player => player.HasFinished);
    }

    private void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }

    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("�Q�[���I���I");
        // ���ʔ��\�V�[���Ɉڍs
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    private IEnumerator OpenRoulette(Player player)
    {
        // ���[���b�g�V�[�����J���Č��ʂ��󂯎��
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Ruretto", LoadSceneMode.Additive);
        Debug.Log("���[���b�g�I�[�v��");
        int result = RouletteResultHandler.GetResult(); // ���̌��ʎ擾�֐�
        player.MoveSteps = result;
        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ruretto");
        Debug.Log("���[���b�g�N���[�Y"+result);
    }

    public IEnumerator OpenRulletoButton()
    {
        Player player = players[currentPlayerIndex];
        yield return StartCoroutine(OpenRoulette(player));
    }
}
