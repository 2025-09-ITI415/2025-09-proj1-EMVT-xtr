using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;
using TMPro; 

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;

	private bool isStopped = false;

    private float stopEndTime = 0f;

	 public TextMeshProUGUI  timerText;
	 private float startTime;
	 private bool timerStarted = false;
    private bool isRunning = true; // whether timer is running
	private bool gameWon = false;
	private float finalTime = 0f;
	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();
		timerText.text = "Time: 0.00s";
	}

	// Each physics step..
	void FixedUpdate ()
	{
		if (isStopped)
        {
            // Check if stop time is over
            if (Time.time >= stopEndTime)
            {
                isStopped = false;
            }
            else
            {
                // Keep player still
                rb.linearVelocity = Vector3.zero;
            }
        }
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		if (!timerStarted && (moveHorizontal != 0f || moveVertical != 0f))
        {
            startTime = Time.time;
            timerStarted = true;
        }

        if (timerStarted && isRunning && !gameWon)
        {
            float timeElapsed = Time.time - startTime;
            timerText.text = "Time: " + timeElapsed.ToString("F2") + "s";
        }

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed);
	}
	void LateUpdate()
{
    // After all physics/movement updates, force final time display if won
    if (gameWon)
    {
        timerText.text = "Final Time: " + finalTime.ToString("F2") + "s";
    }
}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Trap")){
			Debug.Log("TakingDamage");
			other.gameObject.SetActive (false);
			isStopped = true;
            stopEndTime = Time.time + 2f;
            rb.linearVelocity = Vector3.zero;
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
		if (other.gameObject.CompareTag("Win"))
        {
            Debug.Log("You Win!");
            isRunning = false; // stop timer
            finalTime = Time.time - startTime;
			gameWon = true;
			Debug.Log(finalTime);
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