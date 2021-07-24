using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	// Item to give the player
	public Item itemAsset;

	// Very basically adding this item to the player's inventory
	public void Collect()
	{
		FindObjectOfType<PlayerInventory>().Add(itemAsset);
		FindObjectOfType<InventoryUi>().RefreshInventory();
		Destroy(gameObject);
	}
}
