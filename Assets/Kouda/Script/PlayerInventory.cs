using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject textPrefab;
    public Transform InventoryPanel;

    public void Inventory(Player player)
    {
        foreach(Transform child in InventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var Ingredient in player.Ingredients)
        {
            GameObject textObj = Instantiate(textPrefab, InventoryPanel);
            Text text = textObj.GetComponent<Text>();

            text.text = $"{Ingredient.Name}";
        }
    }


}
