using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CustomData/Item", order = 1)]
public class Item : ScriptableObject
{
	// Defining the item type enum
	public enum ItemType
	{
		Consumable,
		Equipment,
		Currency
	}
	// Item unique display name
	public string itemName;
	// Sprite for inventory UI
	public Sprite itemImage;
	// Enum item type
	public ItemType itemType;
	// Prefab used to physically
	// represent the object in the world
	public GameObject itemPrefab;
	// The usable ScriptableObject
	public AbstractUsableItem itemUsable;
	// Is item stackable
	public bool itemStackable = false;
	// Item quantity amount for
	// stackable items
	public int itemQuantity = 1;
}
