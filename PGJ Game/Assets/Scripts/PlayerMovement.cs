using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float maxChargeTime = 2.0f;
	public float powerMultiplier = 10.0f;
	public float brakeForce = 0.1f;
	public float killSpeedThreshold = 10f;
	public GameObject endScreen;
	public static List<GameObject> enemies = new();
	public int score;
	public int coins;
	public PlayerData playerData;
	public int moveLimit;

	private Rigidbody2D rb;
	private float chargeStartTime = 0.0f;
	private SpriteRenderer spriteRenderer;
	private float startTime;
	private float endTime;
	private float totalEnemyKillTime;
	private int moveCount;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		var enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in enemyObjects) {
			enemies.Add(enemy);
		}

		startTime = Time.time;
	}

	void Update() {
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = mousePosition - transform.position;
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		rb.rotation = angle;

		if (Input.GetMouseButtonDown(0)) {
			chargeStartTime = Time.time;
		}

		if (Input.GetMouseButtonUp(0)) {
			if (Time.time - chargeStartTime > 0.0f) {
				var chargeRatio = Mathf.Clamp01((Time.time - chargeStartTime) / maxChargeTime);
				var forceMagnitude = chargeRatio * powerMultiplier;
				direction.Normalize();
				rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);

				// Increment moveCount when the player makes a move
				moveCount++;

				// If moveCount is greater than or equal to moveLimit, end the game
				if (moveCount >= moveLimit) {
					print("Game Over!");
					//EndGame();
				}
			}
		}

		if (rb.velocity.magnitude > 0) {
			rb.AddForce(-rb.velocity * brakeForce);
		}

		if (mousePosition.x < transform.position.x) {
			spriteRenderer.flipY = true;
		}
		else {
			spriteRenderer.flipY = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			if (rb.velocity.magnitude > killSpeedThreshold) {
				score++;
				enemies.Remove(other.gameObject);
				totalEnemyKillTime += Time.time - startTime;
				Destroy(other.gameObject);
			}
			else {
				rb.velocity = -rb.velocity;
			}
		}

		if (other.gameObject.CompareTag("Coins")) {
			coins++;
			Destroy(other.gameObject);
		}

		if (other.gameObject.CompareTag("End") && enemies.Count == 0) {
			endTime = Time.time;
			endScreen.SetActive(true);
			StartCoroutine(LerpTimeScaleToZero(1));
			print(CalculateScore());
		}
	}

	IEnumerator LerpTimeScaleToZero(float duration) {
		var start = Time.timeScale;
		var elapsed = 0f;

		while (elapsed < duration) {
			elapsed += Time.unscaledDeltaTime;
			Time.timeScale = Mathf.Lerp(start, 0f, elapsed / duration);
			yield return null;
		}

		Time.timeScale = 0f;
	}

	public float CalculateScore() {
		var timeTaken = endTime - startTime;
		var enemyPoints = enemies.Count * 100;
		var coinPoints = coins * 50;
		var timePenalty = timeTaken * 1;

		var finalScore = enemyPoints + coinPoints - timePenalty;

		finalScore = Mathf.Max(finalScore, 0);

		playerData.ScoreUI = "Score: " + finalScore;

		return finalScore;
	}
}