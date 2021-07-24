using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	// Inventory sizes
	public int inventorySize = 12;
	public int equipmentSize = 3;
	// Inventory lists
	public List<Item> inventory = new List<Item>();
	public List<Item> equips = new List<Item>();

	// Adding an item to the inventory
	public void Add(Item item)
	{
		Item itemToAdd = item;
		// Stackable items need to be dealt with slightly differently
		if (item.itemStackable)
		{
			// The stored stackable item needs to be a duplicate
			// so that the quantity field can be safely changed
			itemToAdd = Instantiate(item);
			Item existingItem = inventory.Find(x => x.itemName == item.itemName);
			if (existingItem != null)
			{
				existingItem.itemQuantity += itemToAdd.itemQuantity;
				// If just adding quantity, skip the rest of the method
				return;
			}
		}
		// If the inventory is full,
		// add the item first then drop it
		inventory.Add(itemToAdd);
		if (inventory.Count > inventorySize)
		{
			Drop(itemToAdd);
		}
		SortInventories();
	}

	// Dropping an item from the inventory
	public void Drop(Item item)
	{
		// Creating a new GameObject for the dropped item
		// and using the ItemSpawner component to set it up
		GameObject drop = new GameObject();
		drop.transform.position = transform.position;
		drop.transform.position += Vector3.up + Vector3.forward;
		drop.transform.RotateAround(transform.position, Vector3.up, Random.Range(0.0f, 360.0f));
		ItemSpawner spawner = drop.AddComponent<ItemSpawner>();
		spawner.itemAsset = item;
		// Need to remove and unequip the item from inventories
		if (equips.Contains(item))
		{
			item.itemUsable.OnItemUnequip();
			equips.Remove(item);
		}
		else
		{
			inventory.Remove(item);
		}
		SortInventories();
	}

	// Using an item
	public void Use(Item item)
	{
		item.itemUsable.UseItem();
		inventory.Remove(item);
		SortInventories();
	}

	// Equipping an item
	public void Equip(Item item)
	{
		if (equips.Count >= equipmentSize)
		{
			Debug.Log("Equipment full");
		}
		else
		{
			item.itemUsable.OnItemEquip();
			equips.Add(item);
			inventory.Remove(item);
		}
		SortInventories();
	}

	// Unequipping an item
	public void Unequip(Item item)
	{
		if (inventory.Count >= inventorySize)
		{
			Debug.Log("Inventory full");
		}
		else
		{
			item.itemUsable.OnItemUnequip();
			equips.Remove(item);
			inventory.Add(item);
		}
		SortInventories();
	}

	// Used to always sort the inventories alphabetically
	public void SortInventories()
	{
		inventory.Sort((x, y) => x.itemName.CompareTo(y.itemName));
		equips.Sort((x, y) => x.itemName.CompareTo(y.itemName));
	}
}
