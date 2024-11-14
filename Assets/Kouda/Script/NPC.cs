using UnityEngine;

public enum NPCStrength
{
    Weak,
    Normal,
    Strong
}

public class NPC : MonoBehaviour
{
    public NPCStrength npcStrength;  // NPCの強さ
    public int playerIndex;          // プレイヤーのインデックス（NPCが何番目か）

    // ルート選択の処理
    public void ChooseRoute()
    {
        switch (npcStrength)
        {
            case NPCStrength.Weak:
                ChooseRandomRoute();
                break;
            case NPCStrength.Normal:
                ChooseBasedOnIngredients();
                break;
            case NPCStrength.Strong:
                ChooseBasedOnIngredientsWithKeyRoute();
                break;
        }
    }

    // 弱いNPCのルート選択（完全ランダム）
    private void ChooseRandomRoute()
    {
        Debug.Log($"NPC {playerIndex} is choosing a random route.");
        // ランダムなルート選択処理を実装
    }

    // 普通のNPCのルート選択（食材に基づいて選択）
    private void ChooseBasedOnIngredients()
    {
        Debug.Log($"NPC {playerIndex} is choosing a route based on ingredients.");
        // 食材に基づいたルート選択処理を実装（鍵ルートは選ばない）
    }

    // 強いNPCのルート選択（食材に基づいて選択し、鍵ルートも使う）
    private void ChooseBasedOnIngredientsWithKeyRoute()
    {
        Debug.Log($"NPC {playerIndex} is choosing a route with keys.");
        // 食材に基づいたルート選択処理（鍵ルートを含む）
    }

    // ミニゲームでの強さ（実装例として強さに応じたダメージなど）
    public void PlayMiniGame()
    {
        switch (npcStrength)
        {
            case NPCStrength.Weak:
                HandleWeakMiniGame();
                break;
            case NPCStrength.Normal:
                HandleNormalMiniGame();
                break;
            case NPCStrength.Strong:
                HandleStrongMiniGame();
                break;
        }
    }

    // 弱いNPCのミニゲーム
    private void HandleWeakMiniGame()
    {
        Debug.Log($"NPC {playerIndex} is very weak in mini-game.");
        // ミニゲームでの弱いNPCの挙動（すぐに倒される）
    }

    // 普通のNPCのミニゲーム
    private void HandleNormalMiniGame()
    {
        Debug.Log($"NPC {playerIndex} is moderately strong in mini-game.");
        // ミニゲームでの普通のNPCの挙動（倒しやすい）
    }

    // 強いNPCのミニゲーム
    private void HandleStrongMiniGame()
    {
        Debug.Log($"NPC {playerIndex} is a strong opponent in mini-game.");
        // ミニゲームでの強いNPCの挙動（強敵）
    }
}
