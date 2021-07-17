using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// You need to add the Events namespace
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
	// UnityEvents are very handy for adding method
	// calls that can be changed in the inspector
	public UnityEvent enterEvent;
	public UnityEvent exitEvent;

	// OnTriggerEnter/Exit are called if the GameObject
	// has a Collider with Is Trigger enabled and a
	// GameObject with a Rigidbody and a Collider hits it
	private void OnTriggerEnter(Collider other)
	{
		// Optionally you can check that it's the player touching
		// the trigger by checking the for the correct applied tag
		if (other.gameObject.tag == "Player")
		{
			// Invoke calls the method on the GameObject assinged
			// to the UnityEvent in the inspector
			enterEvent.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			exitEvent.Invoke();
		}
	}
}
