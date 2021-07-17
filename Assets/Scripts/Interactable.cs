using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Using UnityEvents
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	// Multiline gives a bigger textbox in Inspector
	[Multiline]
	public string interactText;
	public UnityEvent interactEvent = new UnityEvent();
	// An abbreviated method for calling the UnityEvent
	public void Interact() { if (interactEvent != null) { interactEvent.Invoke(); } }

	private void OnDisable()
	{
		FindObjectOfType<InteractController>().interactableObjects.Remove(this);
	}
}
