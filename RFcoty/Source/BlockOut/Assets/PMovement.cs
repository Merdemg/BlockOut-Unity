using UnityEngine;
using System.Collections;

public class PMovement : MonoBehaviour {
	[SerializeField] private float speed = 1;
	[SerializeField] private Vector3 direction;
	
	public float SpeedPerFrame
	{
		get { return this.speed * Time.deltaTime; }
	}
	
	
	#region MonoBehaviour
	
	void Update ()
	{
		// Determine in which direction the player is moving.
		this.direction = DetectKeyboardInput();
		
		// Move the player in that direction.
		Move (this.direction);
	}
	
	#endregion MonoBehaviour
	
	
	private Vector3 DetectKeyboardInput ()
	{
		// Set our direction vector to zero.
		// Here we use Vector2.zero, which is equivalent to (0, 0).
		Vector3 movementDirection = Vector3.zero;
		
		// Check which arrow key is pressed.
		// If all four arrow keys are pressed, the resulting vector will be (0, 0). 
		// This is because all four direction vectors added together cancel each other out.
		// For example: 
		// Pressing left and right keys results in (1, 0) + (-1, 0) = (0, 0)
		// Pressing up and down keys results in (0, 1) + (0, -1) = (0, 0)
		// Pressing all four direction keys results in (1, 0) + (-1, 0) + (0, 1) + (0, -1) = (0, 0)
		// This also means that pressing right and up key at the same time results in diagonal motion: 
		// (1, 0) + (0, 1) = (1, 1)
		if (Input.GetKey(KeyCode.UpArrow))
		{
			// Add direction vector (0, 1) to movement direction.
			movementDirection += Vector3.forward;
		} 
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			// Add direction vector (0, -1) to movement direction.
			movementDirection += -1 * Vector3.forward;
		}
		// Return the resulting movement direction, based on the keyboard input.
		return movementDirection;
	}
	
	/// <summary>
	/// Move this object towards the specified movement direction.
	/// </summary>
	/// <param name="movementDirection">Movement direction.</param>
	private void Move (Vector3 movementDirection)
	{
		// Move my game object in the specified direction.
		// Here we use += assignment operator which adds what's on the left side of the operator to what's on the left.
		// In other words, the amount by which this object moves this frame is added to the object's current position.
		// The amount of movement is provided by the SpeedPerFrame property.
		// The direction of movement is provided by the movementDirection parameter.
		this.gameObject.transform.localPosition 
			+= ((Vector3)movementDirection * this.SpeedPerFrame);
		
		// Alternatively, we could have written the above line as:
		// this.gameObject.transform.localPosition = 
		// 			this.gameObject.transform.localPosition + ((Vector3)movementDirection * this.SpeedPerFrame);
	}
}
