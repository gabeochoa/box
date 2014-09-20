using UnityEngine;
using System.Collections;

public class PlayerMvt : MonoBehaviour {
	
	// This component is only enabled for "my player" (i.e. the character belonging to the local client machine).
	// Remote players figures will be moved by a NetworkCharacter, which is also responsible for sending "my player's"
	// location to other computers.
	
	public float speed = 10f;		// The speed at which I run

	// Booking variables
	Vector3 direction = Vector3.zero;	// forward/back & left/right
	float   verticalVelocity = 0;		// up/down
	
	CharacterController cc;
	
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		// WASD forward/back & left/right movement is stored in "direction"
		direction = transform.rotation * new Vector3( Input.GetAxis("Horizontal") , 0, Input.GetAxis("Vertical") );
		
		if(direction.magnitude > 1f) {
			direction = direction.normalized;
		}

	}
	
	// FixedUpdate is called once per physics loop
	// Do all MOVEMENT and other physics stuff here.
	void FixedUpdate () {
		
		// "direction" is the desired movement direction, based on our player's input
		Vector3 dist = direction * speed * Time.deltaTime;
		
		if(cc.isGrounded && verticalVelocity < 0) {
			verticalVelocity = Physics.gravity.y * Time.deltaTime;
		}
		else {
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
		}
		
		// Add our verticalVelocity to our actual movement for this frame
		dist.y = verticalVelocity * Time.deltaTime;
		
		// Apply the movement to our character controller (which handles collisions for us)
		cc.Move( dist );
	}
}
