using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public int inventorySize = 12;
	public int equipmentSize = 3;
	public List<Item> inventory = new List<Item>();
	public List<Item> equips = new List<Item>();

	public void Add(Item item)
	{
		Item itemToAdd = item;
		if (item.itemStackable)
		{
			itemToAdd = Instantiate(item);
			Item existingItem = inventory.Find(x => x.itemName == item.itemName);
			if (existingItem != null)
			{
				existingItem.itemQuantity += itemToAdd.itemQuantity;
				return;
			}
		}
		inventory.Add(itemToAdd);
		if (inventory.Count > inventorySize)
		{
			Drop(itemToAdd);
		}
		SortInventories();
	}

	public void Drop(Item item)
	{
		GameObject drop = new GameObject();
		drop.transform.position = transform.position;
		drop.transform.position += Vector3.up + Vector3.forward;
		drop.transform.RotateAround(transform.position, Vector3.up, Random.Range(0.0f, 360.0f));
		ItemSpawner spawner = drop.AddComponent<ItemSpawner>();
		spawner.itemAsset = item;
		if (equips.Contains(item))
		{
			item.itemUsable.OnItemUnequip();
		}
		inventory.Remove(item);
		equips.Remove(item);
		SortInventories();
	}

	public void Use(Item item)
	{
		item.itemUsable.UseItem();
		inventory.Remove(item);
		SortInventories();
	}

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

	public void SortInventories()
	{
		inventory.Sort((x, y) => x.itemName.CompareTo(y.itemName));
		equips.Sort((x, y) => x.itemName.CompareTo(y.itemName));
	}
}
