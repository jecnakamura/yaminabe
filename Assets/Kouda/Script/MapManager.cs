using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<Tile> tiles; // マップ上のすべてのマス
    public Dictionary<Player, int> playerPositions = new Dictionary<Player, int>();

    public void MovePlayer(Player player)
    {
        int currentPos = playerPositions[player];
        int targetPos = Mathf.Clamp(currentPos + player.MoveSteps, 0, tiles.Count - 1);

        playerPositions[player] = targetPos;

        // アニメーションで移動する場合
        StartCoroutine(MovePlayerAnimation(player, tiles[targetPos].transform.position));
    }

    private IEnumerator MovePlayerAnimation(Player player, Vector3 targetPosition)
    {
        float duration = 1.0f;
        float elapsedTime = 0;

        Vector3 startPosition = player.transform.position;

        while (elapsedTime < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition;
    }

    public IEnumerator HandleTileEvent(Player player, List<Player> allPlayers)
    {
        int currentPos = playerPositions[player];
        Tile currentTile = tiles[currentPos];
        currentTile.ExecuteEvent(player, allPlayers);
        yield return null;
    }
}
