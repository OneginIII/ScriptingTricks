using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, ISelectHandler
{
	private InventoryUi inventoryUi;
	public Item currentItem = null;

	private void Start()
	{
		inventoryUi = FindObjectOfType<InventoryUi>();
	}

	public void OnSelect(BaseEventData eventData)
	{
		inventoryUi.OnCurrentSlotSet(GetComponent<InventorySlot>());
	}

	public void SetSlotItem(Item item)
	{
		Image img = transform.Find("Icon").GetComponent<Image>();
		if (item != null)
		{
			currentItem = item;
			img.enabled = true;
			img.sprite = currentItem.itemImage;
		}
		else
		{
			img.enabled = false;
			currentItem = null;
		}
	}
}
