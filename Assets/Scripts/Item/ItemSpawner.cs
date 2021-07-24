using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSpawner : MonoBehaviour
{
	// ScriptableObject that the item is based on
	public Item itemAsset;
	private GameObject itemInstance;

	// Just calling SpawnItem
	private void Start()
	{
		SpawnItem();
	}

	// This method prepares a physical, collectable item
	public void SpawnItem()
	{
		// Using the prefab as a base
		GameObject prefab = itemAsset.itemPrefab;
		itemInstance = Instantiate(prefab, transform);
		itemInstance.transform.SetParent(null);
		// Making the Interact trigger shape
		SphereCollider sphereCol = itemInstance.AddComponent<SphereCollider>();
		sphereCol.radius = 1.0f / itemInstance.transform.localScale.x;
		sphereCol.isTrigger = true;
		// Setting up the Interactable component
		Interactable interact = itemInstance.AddComponent<Interactable>();
		// Setting up the text for the Interactable component
		string collectText = "Collect " + "<sprite=\"" + itemAsset.itemImage.name + "\" index=0> ";
		if (itemAsset.itemQuantity > 1) { collectText += itemAsset.itemQuantity.ToString() + " " + itemAsset.itemName; }
		else { collectText += itemAsset.itemName; }
		interact.interactText = collectText;
		// Add a CollectibleItem component
		CollectibleItem itemCol = itemInstance.AddComponent<CollectibleItem>();
		itemCol.itemAsset = itemAsset;
		// Setup collision ignoring for the player's colliders
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
		// Setting up collection event
		interact.interactEvent.AddListener(itemCol.Collect);
		itemInstance.name = itemAsset.itemName;
		// Once done get rid of this spawning object
		Destroy(gameObject);
	}
}
