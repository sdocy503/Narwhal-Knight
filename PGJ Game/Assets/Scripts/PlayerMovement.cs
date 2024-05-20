using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
	public float maxChargeTime = 2.0f;
	public float powerMultiplier = 10.0f;
	public float brakeForce = 0.1f;
	public float killSpeedThreshold = 10f;
	public EndScreen endScreen;
	public static List<GameObject> enemies = new();
	public int score;
	public int coins;
	public PlayerData playerData;
	public int moveLimit;
	public int totalCoins;
	public float levelTimeLimit = 60;
	public MoveCounter moveCounter;
	public AudioClip killSound, dashSound, coinPickupSound;
	
	public int currentLevel = 0;
	public List<float> maxScoresPerLevel = new() {0, 45, 140, 140, 275};

	private Animator anim;
	private Rigidbody2D rb;
	private float chargeStartTime = 0.0f;
	private SpriteRenderer spriteRenderer;
	private float startTime;
	private float endTime;
	private int moveCount;
	private float levelStartTime;
	private int enemiesKilled;
	private int coinsCollected;
	private AudioSource audio;
	
	
	
	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponentInChildren<Animator>();
		audio = GetComponent<AudioSource>();
		anim.SetBool("Idle", true);
		var enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in enemyObjects) {
			enemies.Add(enemy);
		}

		startTime = Time.time;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDestroy() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void Start() {
		Time.timeScale = 1;
		levelStartTime = Time.time;
		moveCount = 0;
		playerData.MoveCount = moveCount;
	}
	
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		currentLevel = scene.buildIndex;
	}

	void Update() {
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = mousePosition - transform.position;
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		rb.rotation = angle;

		if (moveCount < moveLimit) {
			if (Input.GetMouseButtonDown(0)) {
				chargeStartTime = Time.time;
				anim.SetBool("IsCharging", true);
			}

			if (Input.GetMouseButtonUp(0)) {
				if (Time.time - chargeStartTime > 0.0f) {
					var chargeRatio = Mathf.Clamp01((Time.time - chargeStartTime) / maxChargeTime);
					var forceMagnitude = chargeRatio * powerMultiplier;
					direction.Normalize();
					rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
					audio.PlayOneShot(dashSound, 0.35f);
					moveCount++;
					playerData.MoveCount = moveCount;
					moveCounter.UpdateMoveCount();
					if (moveCount >= moveLimit) {
						print("Game Over!");
						endScreen.Defeat();
						StartCoroutine(LerpTimeScaleToZero(1));
					}

					anim.SetBool("IsCharging", false);
					anim.SetTrigger("Charge");
				}
			}
		}

		if (rb.velocity.magnitude > 0) {
			rb.AddForce(-rb.velocity * brakeForce);
		}
		else {
			anim.SetBool("Idle", true);
		}

		if (mousePosition.x < transform.position.x) {
			spriteRenderer.flipY = true;
		}
		else {
			spriteRenderer.flipY = false;
		}

		if (Time.time - levelStartTime > levelTimeLimit) {
			print("Game Over!");
			endScreen.Defeat();
			StartCoroutine(LerpTimeScaleToZero(1));
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			if (rb.velocity.magnitude > killSpeedThreshold) {
				enemiesKilled++;
				enemies.Remove(other.gameObject);
				audio.PlayOneShot(killSound);

				// Find the enemy object with the EnemyDying script attached to it
				var enemyDying = other.gameObject.GetComponent<EnemyDying>();
				if (enemyDying != null) {
					// Call the Die method before destroying the enemy object
					enemyDying.Die();
					Destroy(other.gameObject, 1f); // Delay the destruction to allow the fade out effect to complete
				}
			}
			else {
				rb.velocity = -rb.velocity;
			}
		}

		if (other.gameObject.CompareTag("Coins")) {
			coinsCollected++;
			audio.PlayOneShot(coinPickupSound);
			Destroy(other.gameObject);
		}

		if (other.gameObject.CompareTag("End") && enemies.Count == 0) {
			endTime = Time.time;
			endScreen.Victory();
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
		var timeTaken = Time.time - levelStartTime;
		var enemyPoints = enemiesKilled * 100;
		var coinPoints = coinsCollected * 50;

		var finalScore = enemyPoints + coinPoints - timeTaken;

		finalScore = Mathf.Max(finalScore, 0);

		playerData.ScoreUI = "Score: " + finalScore;

		return finalScore;
	}

	public void ResetTimeScale() {
		Time.timeScale = 1;
	}
}