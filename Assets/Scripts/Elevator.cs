using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public float speed = 10.0f;
	// The endPos will be public and editable
	// but the startPos will just be assigned
	// in Start to be the initial position
	private Vector3 startPos;
	public Vector3 endPos;
	private Vector3 targetPos;
	// This is the time the elevator stops for
	// on each end before moving back
	public float waitTime = 1.0f;

	// The internal timer float used to track
	// the progress of time
	private float timer = 0.0f;
	// These two booleans are needed for
	// the direction and stopping logic
	private bool stopped = true;
	private bool movingToEnd = true;
	private Rigidbody rb;

	private void Start()
	{
		// Setting start position
		startPos = transform.position;
		rb = GetComponent<Rigidbody>();
		// Setting the first target position
		targetPos = endPos;
	}

	private void StartElevator()
	{
		// This method unstops the elevator,
		// switches direction and resets the timer
		stopped = false;
		movingToEnd = !movingToEnd;
		timer = waitTime;
		// Here the target position is set based on the next direction
		if (movingToEnd) { targetPos = endPos; }
		else { targetPos = startPos; }
	}

	private void FixedUpdate()
	{
		// The elevator has two main states: stopped and moving determined by this if/else
		if (stopped)
		{
			// In the stopped state the timer runs down until it is less than zero
			// This abbreviated if/else statement then triggers the elevator to start again
			if (timer > 0.0f) { timer -= Time.deltaTime; }
			else { StartElevator(); }
		}
		else
		{
			// Moving the elevator using the Rigidbody component's MovePosition method
			// and Vector3's MoveTowards to calculate the target position based on speed * delta
			rb.MovePosition(Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed));
			// Yes. Calling this seemingly reduntant transform updates the position so that the player
			// sticks to the elevator. This was the only way I found of doing this so that the motion
			// is fairly smooth :/
			transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
			// The == operator checks that these two Vector3's are approximately equal and stops the elevator
			if (transform.position == targetPos) { stopped = true; }
		}
	}

	// You need to enable kinematic + kinematic physics contact pairs in project settings!!!

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			// Setting the parent. The second parameter cancels out any modifications
			// done to the elevator's position/rotation/scale on our player.
			col.transform.SetParent(this.transform, true);
		}
	}

	private void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.transform.SetParent(null);
		}
	}
}
