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
		inventory.Add(item);
	}

	public void Drop(Item item)
	{
		GameObject drop = new GameObject();
		drop.transform.position = transform.position;
		drop.transform.position += Vector3.up + Vector3.forward;
		drop.transform.RotateAround(transform.position, Vector3.up, Random.Range(0.0f, 360.0f));
		ItemSpawner spawner = drop.AddComponent<ItemSpawner>();
		spawner.itemAsset = item;
		inventory.Remove(item);
		equips.Remove(item);
	}

	public void Use(Item item)
	{
		item.itemUsable.UseItem();
		inventory.Remove(item);
	}

	public void Equip(Item item)
	{
		equips.Add(item);
		inventory.Remove(item);
	}

	public void Unequip(Item item)
	{
		equips.Remove(item);
		inventory.Add(item);
	}
}
