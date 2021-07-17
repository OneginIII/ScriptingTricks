using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
	private GameObject panel;

	private void Start()
	{
		panel = transform.GetChild(0).gameObject;
		panel.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Menu"))
		{
			panel.SetActive(true);
		}
	}
}
