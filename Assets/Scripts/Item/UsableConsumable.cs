using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UsableItem", menuName = "CustomData/UsableConsumable", order = 1)]
public class UsableConsumable : AbstractUsableItem
{
	public int hpAdd = 0;
	public int mpAdd = 0;

	public override void UseItem()
	{
		if (hpAdd != 0)
		{
			FindObjectOfType<StatusController>().Health += hpAdd;
		}
		if (mpAdd != 0)
		{
			FindObjectOfType<StatusController>().Mana += mpAdd;
		}
	}
}
