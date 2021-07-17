using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public List<Item> items = new List<Item>();

	public void Add(Item item)
	{
		items.Add(item);
	}
}
