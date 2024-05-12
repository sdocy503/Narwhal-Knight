using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float maxChargeTime = 2.0f;
	public float powerMultiplier = 10.0f;
	public float brakeForce = 0.1f;

	private Rigidbody2D rb;
	private float chargeStartTime = 0.0f;
	private SpriteRenderer spriteRenderer;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() {
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = mousePosition - transform.position;
		// Calculate the angle between the player and the mouse position
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		rb.rotation = angle;

		if (Input.GetMouseButtonDown(0)) {
			chargeStartTime = Time.time;
		}

		if (Input.GetMouseButtonUp(0)) {
			// If the player has been charging for at least 0.1 seconds, apply a force to the player
			if (Time.time - chargeStartTime > 0.0f) {
				var chargeRatio = Mathf.Clamp01((Time.time - chargeStartTime) / maxChargeTime);
				var forceMagnitude = chargeRatio * powerMultiplier;
				// Normalize the direction vector to ensure that the player moves at a consistent speed
				direction.Normalize();
				// Apply the force to the player
				rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
			}
		}
		// Apply a brake force to the player if the player is moving, just so the player doesn't keep moving forever
		if (rb.velocity.magnitude > 0)
		{
			rb.AddForce(-rb.velocity * brakeForce);
		}

		// Flip the player sprite based on the mouse position
		if (mousePosition.x < transform.position.x) {
			spriteRenderer.flipX = true;
		}
		else {
			spriteRenderer.flipX = false;
		}
	}
}