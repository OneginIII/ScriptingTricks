using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSpawner : MonoBehaviour
{
	public Item itemAsset;
	private GameObject itemInstance;

	private void Start()
	{
		GameObject prefab = itemAsset.itemPrefab;
		itemInstance = Instantiate(prefab, transform);
		itemInstance.transform.SetParent(null);
		SphereCollider sphereCol = itemInstance.AddComponent<SphereCollider>();
		sphereCol.radius = 0.01f;
		sphereCol.isTrigger = true;
		Interactable interact = itemInstance.AddComponent<Interactable>();
		interact.interactText = "Collect " + itemAsset.itemName;
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
