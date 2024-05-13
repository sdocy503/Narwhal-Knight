using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {
	public Transform target;
	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public Vector2 minBound, maxBound; // The minimum and maximum coordinates of the level bounds

	void FixedUpdate() {
		var desiredPosition = target.position + offset;
		var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

		// Clamp the x and y coordinates of smoothedPosition within the level bounds
		smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBound.x, maxBound.x);
		smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBound.y, maxBound.y);

		transform.position = smoothedPosition;
	}
}