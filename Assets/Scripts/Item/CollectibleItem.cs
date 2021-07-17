using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	public Item itemAsset;

	public void Collect()
	{
		FindObjectOfType<PlayerInventory>().Add(itemAsset);
		FindObjectOfType<InventoryUi>().RefreshInventory();
		Destroy(this.gameObject);
	}
}
