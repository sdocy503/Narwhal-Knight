using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
	public GameObject LevelSelect;

	private UIDocument document;
	private Button playButton;

	private void OnEnable()
	{
		document = GetComponent<UIDocument>();
		playButton = document.rootVisualElement.Q("Start") as Button;

		playButton.RegisterCallback<ClickEvent>(OnStartPress);
	}

	private void OnDisable()
	{
		playButton.UnregisterCallback<ClickEvent>(OnStartPress);
	}

	private void OnStartPress(ClickEvent evt)
	{
		// Reset the timescale
		Time.timeScale = 1;

		LevelSelect.SetActive(true);
		gameObject.SetActive(false);
	}
}