using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUi : MonoBehaviour
{
	public Button useButton;
	public Button dropButton;
	private GameObject panel;
	public InventorySlot currentSlot = null;
	private InventorySlot[] invSlots;
	private InventorySlot[] equipSlots;
	private PlayerInventory playerInv;

	private void Start()
	{
		playerInv = FindObjectOfType<PlayerInventory>();
		panel = transform.GetChild(0).gameObject;
		panel.SetActive(false);
		invSlots = panel.transform.Find("InventoryGrid").GetComponentsInChildren<InventorySlot>();
		equipSlots = panel.transform.Find("EquipGrid").GetComponentsInChildren<InventorySlot>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Menu"))
		{
			ToggleInventory();
		}
	}

	public void ToggleInventory()
	{
		currentSlot = null;
		panel.SetActive(!panel.activeInHierarchy);
		RefreshInventory();
		FindObjectOfType<PlayerController>().controlEnabled = !panel.activeInHierarchy;
	}

	public void RefreshInventory()
	{
		for (int i = 0; i < invSlots.Length; i++)
		{
			if (playerInv.inventory.Count > i)
			{
				invSlots[i].SetSlotItem(playerInv.inventory[i]);
			}
			else
			{
				invSlots[i].SetSlotItem(null);
			}
		}
		for (int i = 0; i < equipSlots.Length; i++)
		{
			if (playerInv.equips.Count > i)
			{
				equipSlots[i].SetSlotItem(playerInv.equips[i]);
			}
			else
			{
				equipSlots[i].SetSlotItem(null);
			}
		}
		OnCurrentSlotSet(currentSlot);
	}

	public void OnCurrentSlotSet(InventorySlot slot)
	{
		currentSlot = slot;
		if (currentSlot?.currentItem != null)
		{
			TextMeshProUGUI text = useButton.GetComponentInChildren<TextMeshProUGUI>();
			Item.ItemType type = currentSlot.currentItem.itemType;
			if (type == Item.ItemType.Equipment)
			{
				if (playerInv.equips.Contains(slot.currentItem))
				{
					text.text = "Unequip";
				}
				else
				{
					text.text = "Equip";
				}
			}
			else
			{
				text.text = "Use";
			}
			useButton.interactable = true;
			dropButton.interactable = true;
		}
		else
		{
			useButton.interactable = false;
			dropButton.interactable = false;
		}
	}

	public void OnDropItem()
	{
		if (currentSlot?.currentItem != null)
		{
			playerInv.Drop(currentSlot.currentItem);
			RefreshInventory();
		}
	}

	public void OnUseItem()
	{
		if (currentSlot?.currentItem != null)
		{
			if (currentSlot.currentItem.itemType == Item.ItemType.Consumable)
			{
				playerInv.Use(currentSlot.currentItem);
			}
			else if (currentSlot.currentItem.itemType == Item.ItemType.Equipment)
			{
				if (playerInv.inventory.Contains(currentSlot.currentItem))
				{
					playerInv.Equip(currentSlot.currentItem);
				}
				else if (playerInv.equips.Contains(currentSlot.currentItem))
				{
					playerInv.Unequip(currentSlot.currentItem);
				}
			}
			RefreshInventory();
		}
	}
}
