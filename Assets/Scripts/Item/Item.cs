using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "CustomData/Item", order = 1)]
public class Item : ScriptableObject
{
	public enum ItemType
	{
		Consumable,
		Equipment,
		Currency
	}
	public string itemName;
	public Sprite itemImage;
	public ItemType itemType;
	public GameObject itemPrefab;
	public AbstractUsableItem itemUsable;
}
