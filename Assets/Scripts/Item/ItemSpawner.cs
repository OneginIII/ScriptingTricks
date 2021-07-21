using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSpawner : MonoBehaviour
{
	public Item itemAsset;
	private GameObject itemInstance;
	private Transform parentTransform;

	private void Start()
	{
		SpawnItem();
	}

	public void SpawnItem()
	{
		GameObject prefab = itemAsset.itemPrefab;
		itemInstance = Instantiate(prefab, transform);
		itemInstance.transform.SetParent(null);
		SphereCollider sphereCol = itemInstance.AddComponent<SphereCollider>();
		sphereCol.radius = 1.0f / itemInstance.transform.localScale.x;
		sphereCol.isTrigger = true;
		Interactable interact = itemInstance.AddComponent<Interactable>();
		string collectText = "Collect " + "<sprite=\"" + itemAsset.itemImage.name + "\" index=0> ";
		if (itemAsset.itemQuantity > 1) { collectText += itemAsset.itemQuantity.ToString() + " " + itemAsset.itemName; }
		else { collectText += itemAsset.itemName; }
		interact.interactText = collectText;
		CollectibleItem itemCol = itemInstance.AddComponent<CollectibleItem>();
		itemCol.itemAsset = itemAsset;
		GameObject player = FindObjectOfType<PlayerController>().gameObject;
		foreach (Collider col in interact.GetComponents<Collider>())
		{
			foreach (Collider colP in player.GetComponents<Collider>())
			{
				if (col.isTrigger == false)
				{
					Physics.IgnoreCollision(col, colP, true);
				}
			}
		}
		interact.interactEvent.AddListener(itemCol.Collect);
		itemInstance.name = itemAsset.itemName;
		Destroy(gameObject);
	}
}
