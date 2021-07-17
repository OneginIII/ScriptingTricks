using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParticles : MonoBehaviour
{
	private ParticleSystem pSystem;

	private void Start()
	{
		pSystem = GetComponent<ParticleSystem>();
	}

	public void FireParticles(int pColor)
	{
		var main = pSystem.main;
		switch (pColor)
		{
			case 0:
				main.startColor = Color.red;
				break;
			case 1:
				main.startColor = Color.green;
				break;
			case 2:
				main.startColor = Color.blue;
				break;
			default:
				break;
		}
		pSystem.Stop();
		pSystem.Play();
	}
}
