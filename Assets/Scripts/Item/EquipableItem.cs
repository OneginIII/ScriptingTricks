using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UsableItem", menuName = "CustomData/EquipableItem", order = 1)]
public class EquipableItem : AbstractUsableItem
{
	public float movementSpeedAdd;

	public override void OnItemEquip()
	{
		FindObjectOfType<PlayerController>().movementSpeed += movementSpeedAdd;
	}

	public override void OnItemUnequip()
	{
		FindObjectOfType<PlayerController>().movementSpeed -= movementSpeedAdd;
	}
}
