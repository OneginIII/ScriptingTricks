using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	public Item itemAsset;

	public void Collect()
	{
		FindObjectOfType<PlayerInventory>().Add(itemAsset);
		Destroy(this.gameObject);
	}
}
