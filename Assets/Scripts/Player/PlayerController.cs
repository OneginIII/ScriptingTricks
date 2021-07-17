using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed = 10.0f;
	public float jumpPower = 30.0f;
	public float gravity = -9.8f;
	public float gravityAcceleration = 1.0f;
	[HideInInspector]
	public bool inWater = false;
	private float verticalVector = -1.0f;
	private CharacterController charCont;

	public bool controlEnabled = true;

	private void Start()
	{
		// Getting the CharacterController component
		charCont = GetComponent<CharacterController>();
	}

	private void Update()
	{
		// Creating new local variable to store the movement direction
		Vector3 moveVector = Vector3.zero;
		// Getting the axis values (-1.0 to 1.0) from Unity's default input system
		if (controlEnabled)
		{
			moveVector.x = Input.GetAxis("Horizontal");
			moveVector.z = Input.GetAxis("Vertical");
		}
		// Clamping the movement vector length to 1.0 to prevent strafing being faster
		moveVector = Vector3.ClampMagnitude(moveVector, 1.0f);
		// Multiplying the vector by movement speed
		moveVector *= movementSpeed;
		// After movement manipulations, adding in the gravity value
		if (charCont.isGrounded)
		{
			verticalVector = -1.0f;
			if (Input.GetButtonDown("Jump") && controlEnabled)
			{
				verticalVector = jumpPower;
			}
		}
		else
		{
			verticalVector = Mathf.Lerp(verticalVector, gravity, gravityAcceleration * Time.deltaTime);
		}
		moveVector.y = verticalVector;
		// Slow down all movement by 2 if in water
		if (inWater)
		{
			moveVector /= 2.0f;
		}
		// Moving using the CharacterController component's Move method
		// Remember to multiply by delta time to make movement framerate independent
		charCont.Move(moveVector * Time.deltaTime);
	}
}
