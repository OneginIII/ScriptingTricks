using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthArea : MonoBehaviour
{
	// Amount and timer variables
	public int healthChange = 0;
	public float tickTime = 1.0f;
	private float timer = 0.0f;
	// The HealthController component
	private HealthController hpCont = null;

	// OnTriggerEnter/Exit check for right the component
	// and set the variable to that or null
	private void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<HealthController>() != null)
		{
			hpCont = col.GetComponent<HealthController>();
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.GetComponent<HealthController>() != null)
		{
			hpCont = null;
		}
	}

	// In Update the timer counts down if hpCont is not null
	private void Update()
	{
		if (hpCont)
		{
			if (timer < 0.0f)
			{
				// The one line that triggers all the health
				// related effects thanks to the setter
				hpCont.Health += healthChange;
				timer = tickTime;
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}
	}
}
