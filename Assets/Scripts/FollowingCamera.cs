using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
	public GameObject targetObject;
	public Vector3 offsetVector;
	public float followSpeed;

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, targetObject.transform.position + offsetVector, Time.deltaTime * followSpeed);
	}
}
