using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	// These are just related to
	// the flashing material effect
	private MeshRenderer mesh;
	private Material lerpMaterial;
	private Material playerMaterial;
	public Material damageMaterial;
	public Material healMaterial;
	public float hpLerpSpeed = 3.0f;
	private float hpLerp = 1.0f;
	// A visual health indicator
	private TextMesh hpText;

	// Implementing a C# Property for health

	// The recommended way is to have a private
	// variable to contain the actual value
	private int _health = 0;
	// And the public Health is the way that
	// we want other scripts to interact with
	// the health value
	public int Health
	{
		// Default getter
		get => _health;
		// Setter does various things
		set
		{
			// Heal or damage effect based on new value
			if (_health > value)
			{
				damageEffect();
			}
			else
			{
				healEffect();
			}
			// Actually setting the new value
			// Very important!
			_health = value;
			// Updating visual health counter
			hpText.text = (_health.ToString());
		}
	}

	private void Start()
	{
		mesh = GetComponent<MeshRenderer>();
		// Duplicating the original material for lerping
		playerMaterial = Instantiate(mesh.material);
		// Need to do this to make the effect work
		playerMaterial.EnableKeyword("_EMISSION");
		// A child object contains a TextMesh
		hpText = GetComponentInChildren<TextMesh>();
	}

	private void Update()
	{
		// Lerping between materials based on a timer
		if (hpLerp < 1.0f)
		{
			mesh.material.Lerp(lerpMaterial, playerMaterial, hpLerp);
			hpLerp += Time.deltaTime * hpLerpSpeed;
		}
		else
		{
			// Resetting the material
			mesh.material = playerMaterial;
		}
	}

	// The heal/damage effect methods
	// A bit redundant, but works for this example
	private void healEffect()
	{
		lerpMaterial = healMaterial;
		hpLerp = 0.0f;
	}

	private void damageEffect()
	{
		lerpMaterial = damageMaterial;
		hpLerp = 0.0f;
	}
}
