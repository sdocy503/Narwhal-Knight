using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndScreen : MonoBehaviour {
	const float VictoryMargin = 150f;
	const float DefeatMarginVert = 300f;
	const float DefeatMarginHori = 500f;

	private UIDocument document;
	private Button menuButton;
	private Button retryButton;
	private Button continueButton;
	private Label stateLabel;
	private VisualElement starRow;
	private VisualElement container;
	private PlayerMovement playerMovement;
	private List<VisualElement> stars = new List<VisualElement>();

	private void OnEnable() {
		document = GetComponent<UIDocument>();
		menuButton = document.rootVisualElement.Q<Button>("Menu");
		retryButton = document.rootVisualElement.Q<Button>("Retry");
		continueButton = document.rootVisualElement.Q<Button>("Continue");
		starRow = document.rootVisualElement.Q<VisualElement>("StarRow");
		stateLabel = document.rootVisualElement.Q<Label>("State");
		container = document.rootVisualElement.Q<VisualElement>("Container");
		stars.Add(document.rootVisualElement.Q<VisualElement>("Star1"));
        stars.Add(document.rootVisualElement.Q<VisualElement>("Star2"));
        stars.Add(document.rootVisualElement.Q<VisualElement>("Star3"));

        menuButton.RegisterCallback<ClickEvent>(OnMenuPress);
		retryButton.RegisterCallback<ClickEvent>(OnRetryPress);
		continueButton.RegisterCallback<ClickEvent>(OnContinuePress);
		playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
	}

	private void OnDisable() {
		menuButton.UnregisterCallback<ClickEvent>(OnMenuPress);
		retryButton.UnregisterCallback<ClickEvent>(OnRetryPress);
		continueButton.UnregisterCallback<ClickEvent>(OnContinuePress);
	}

	public void Victory() {
		stateLabel.text = "Victory!";
		starRow.style.display = DisplayStyle.Flex;
		continueButton.style.display = DisplayStyle.Flex;
		container.style.marginBottom = VictoryMargin;
		container.style.marginLeft = VictoryMargin;
		container.style.marginRight = VictoryMargin;
		container.style.marginTop = VictoryMargin;
		container.style.display = DisplayStyle.Flex;
		playerMovement.enabled = false;

		var score = playerMovement.CalculateScore();
		var numOfStars = CalculateStars(score);

		for(var i = 0; i < numOfStars; i++) {
			stars[i].style.unityBackgroundImageTintColor = Color.white;
		}
	}

	private int CalculateStars(float score) {
		var maxScore = playerMovement.maxScoresPerLevel[playerMovement.currentLevel];

		var threeStarScore = maxScore * 0.8f;
		var twoStarScore = maxScore * 0.5f;

		if (score >= threeStarScore) {
			return 3;
		}
		else if (score >= twoStarScore) {
			return 2;
		}
		else {
			return 1;
		}
	}

	public void Defeat() {
		stateLabel.text = "Defeat!";
		starRow.style.display = DisplayStyle.None;
		continueButton.style.display = DisplayStyle.None;
		container.style.marginBottom = DefeatMarginVert;
		container.style.marginLeft = DefeatMarginHori;
		container.style.marginRight = DefeatMarginHori;
		container.style.marginTop = DefeatMarginVert;
		container.style.display = DisplayStyle.Flex;
		playerMovement.enabled = false;
	}

	private void OnMenuPress(ClickEvent evt) {
		SceneManager.LoadScene("MainMenu");
	}

	private void OnRetryPress(ClickEvent evt) {
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		StartCoroutine(LoadSceneWithDelay(currentSceneIndex));
	}

	private void OnContinuePress(ClickEvent evt) {
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var nextSceneIndex = currentSceneIndex + 1;
		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
			StartCoroutine(LoadSceneWithDelay(nextSceneIndex));
		}
		else {
			// If there are no more scenes, load the first scene (scene at index 0)
			StartCoroutine(LoadSceneWithDelay(0));
		}
	}

	IEnumerator LoadSceneWithDelay(int sceneIndex) {
		playerMovement.ResetTimeScale();
		yield return null; // Wait for one frame
		SceneManager.LoadScene(sceneIndex);
	}
}