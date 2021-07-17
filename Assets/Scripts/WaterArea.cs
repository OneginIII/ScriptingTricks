using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArea : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.GetComponent<PlayerController>().inWater = true;
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.GetComponent<PlayerController>().inWater = false;
		}
	}
}
