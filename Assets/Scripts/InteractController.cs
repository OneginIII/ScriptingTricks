using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Updating the UI in this script
using TMPro;

public class InteractController : MonoBehaviour
{
	// Creating a new list to store interactable objects
	public List<Interactable> interactableObjects = new List<Interactable>();
	// A variable to store the closest interactable
	private Interactable closestInteractable = null;
	// Text ui element to update
	private TextMeshProUGUI text;

	private void Start()
	{
		// Getting the component to update the text on
		text = GameObject.Find("UiText").GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		// Updating list of interactible objects if there are any
		if (interactableObjects.Count > 0)
		{
			UpdateClosestInteractable();
		}
		else
		{
			// In no interactables close, reset the variables
			text.text = "";
			closestInteractable = null;
		}
		// The input for interacting with interactible objects
		if (Input.GetButtonDown("Fire1") && closestInteractable != null)
		{
			// Since the closestInteractable is the component itself,
			// just directly call the method
			closestInteractable.Interact();
		}
	}

	// This method iterates through the list of interactable objects
	// to find the one that is closest
	private void UpdateClosestInteractable()
	{
		// Start with a default null Interactable
		Interactable closestObj = null;
		// Infinite distance to start out the distance comparison
		float closestDistance = Mathf.Infinity;
		foreach (Interactable intObj in interactableObjects)
		{
			// Doing the distance calculation here first just once for performance
			float distanceAmount = Vector3.Distance(transform.position, intObj.transform.position);
			if (distanceAmount < closestDistance)
			{
				closestDistance = distanceAmount;
				closestObj = intObj;
			}
		}
		// Once the closest interactable is known, update the ui text
		if (closestObj != null)
		{
			text.text = closestObj.interactText;
		}
		closestInteractable = closestObj;
	}

	// To gather the list of interactable objects,
	// OnTriggerEnter/Exit is used
	private void OnTriggerEnter(Collider col)
	{
		// Check that the other object has the Interactable component
		if (col.gameObject.GetComponent<Interactable>() != null)
		{
			// Check if the list contains this Interactible already
			// before adding it in (prevents duplicates)
			Interactable intObj = col.gameObject.GetComponent<Interactable>();
			if (!interactableObjects.Contains(intObj))
			{
				interactableObjects.Add(intObj);
			}
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.GetComponent<Interactable>() != null)
		{
			// Doing the opposite of OnTriggerEnter
			Interactable intObj = col.gameObject.GetComponent<Interactable>();
			if (interactableObjects.Contains(intObj))
			{
				interactableObjects.Remove(intObj);
			}
		}
	}
}
