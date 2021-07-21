using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUsableItem : ScriptableObject
{
	public virtual void UseItem() { }

	public virtual void OnItemEquip() { }

	public virtual void OnItemUnequip() { }
}
