using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract base class for all usable ScriptableObjects
public abstract class AbstractUsableItem : ScriptableObject
{
	// Virtual methods can (but don't have to) be overriden
	public virtual void UseItem() { }

	public virtual void OnItemEquip() { }

	public virtual void OnItemUnequip() { }
}
