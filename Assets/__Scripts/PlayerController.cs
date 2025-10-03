using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();
	}

	// Each physics step..
	void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed);
	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Trap")){
			Debug.Log("TakingDamage");
		}
		if (other.gameObject.CompareTag("SpeedBump")){
			Debug.Log("SpeedBUMPFOOOOO");
			speed = 10;
			rb.linearVelocity *= .4f;
		}
		if (other.gameObject.CompareTag("FastyBump")){
			Debug.Log("GOTTAGOFAST");
			speed = 30;
		}	
		if (other.gameObject.CompareTag("Respawn")){
			Debug.Log("UfellOffLOOL");
			transform.position = new Vector3(0f, 2f, 0f);
			rb.linearVelocity *= 0f;
			speed = 10;

		}
		
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		/*if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}*/
	}


}