using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	private Animation anim;
	private void Start()
	{
		anim = GetComponent<Animation>();
	}
	public void OpenDoor()
	{
		anim.CrossFade("Open", 0.25f);
	}

	public void CloseDoor()
	{
		anim.CrossFade("Close", 0.25f);
	}
}
