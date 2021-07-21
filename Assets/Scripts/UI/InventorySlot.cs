using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, ISelectHandler
{
	private InventoryUi inventoryUi;
	private TextMeshProUGUI quantityText;
	public Item currentItem = null;

	private void Start()
	{
		inventoryUi = FindObjectOfType<InventoryUi>();
		quantityText = GetComponentInChildren<TextMeshProUGUI>();
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
			if (item.itemStackable)
			{
				quantityText.text = item.itemQuantity.ToString();
			}
			else
			{
				quantityText.text = "";
			}
		}
		else
		{
			img.enabled = false;
			currentItem = null;
			quantityText.text = "";
		}
	}
}
