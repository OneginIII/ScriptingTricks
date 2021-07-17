using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
	// These are just related to
	// the flashing material effect
	private MeshRenderer mesh;
	private Material lerpMaterial;
	private Material playerMaterial;
	public Material damageMaterial;
	public Material healMaterial;
	public Material manaMaterial;
	public float matLerpSpeed = 3.0f;
	private float matLerp = 1.0f;
	// A visual health indicator
	private Slider hpSlider;
	private Slider mpSlider;

	// Implementing a C# Property for health

	// The recommended way is to have a private
	// variable to contain the actual value
	private int _health = 50;
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
			hpSlider.value = _health;
		}
	}
	private int _mana = 50;
	public int Mana
	{
		get => _mana;
		set
		{
			if (_mana < value)
			{
				manaEffect();
			}
			_mana = value;
			mpSlider.value = _mana;
		}
	}

	private void Start()
	{
		mesh = GetComponent<MeshRenderer>();
		// Duplicating the original material for lerping
		playerMaterial = Instantiate(mesh.material);
		// Need to do this to make the effect work
		playerMaterial.EnableKeyword("_EMISSION");
		RectTransform canvas = GetComponentInChildren<RectTransform>();
		hpSlider = canvas.GetChild(0).GetComponent<Slider>();
		mpSlider = canvas.GetChild(1).GetComponent<Slider>();
		hpSlider.value = _health;
		mpSlider.value = _mana;
	}

	private void Update()
	{
		// Lerping between materials based on a timer
		if (matLerp < 1.0f)
		{
			mesh.material.Lerp(lerpMaterial, playerMaterial, matLerp);
			matLerp += Time.deltaTime * matLerpSpeed;
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
		matLerp = 0.0f;
	}

	private void damageEffect()
	{
		lerpMaterial = damageMaterial;
		matLerp = 0.0f;
	}

	private void manaEffect()
	{
		lerpMaterial = manaMaterial;
		matLerp = 0.0f;
	}
}
