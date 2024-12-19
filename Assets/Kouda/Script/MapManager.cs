using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapManager : MonoBehaviour
{
    public List<Tile> tiles; // �}�b�v��̂��ׂẴ}�X
    public Dictionary<Player, int> playerPositions = new Dictionary<Player, int>();
    public float MoveDuration = 0.4f;

    public IEnumerator MovePlayer(Player player,int steps)
    {
        int currentPos = playerPositions[player];
        int targetPos = Mathf.Clamp(currentPos + steps, 0, tiles.Count - 1);

        playerPositions[player] = targetPos;

        // �A�j���[�V�����ňړ�����ꍇ
        //yield return StartCoroutine(MovePlayerAnimation(player, tiles[targetPos].transform.position));
        yield return null;
    }

    public  IEnumerator MovePlayerAnimation(Player player, Vector3 targetPosition)
    {
        player.transform.DOMove(targetPosition, MoveDuration).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(MoveDuration);
    }

    public IEnumerator HandleTileEvent(Player player, List<Player> allPlayers)
    {
        int currentPos = playerPositions[player];
        Tile currentTile = tiles[currentPos];
        currentTile.ExecuteEvent(player, allPlayers);
        yield return null;
    }
}
