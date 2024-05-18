using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndScreen : MonoBehaviour
{
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

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();
        menuButton = document.rootVisualElement.Q<Button>("Menu");
        retryButton = document.rootVisualElement.Q<Button>("Retry");
        continueButton = document.rootVisualElement.Q<Button>("Continue");
        starRow = document.rootVisualElement.Q<VisualElement>("StarRow");
        stateLabel = document.rootVisualElement.Q<Label>("State");
        container = document.rootVisualElement.Q<VisualElement>("Container");

        menuButton.RegisterCallback<ClickEvent>(OnMenuPress);
        retryButton.RegisterCallback<ClickEvent>(OnRetryPress);
        continueButton.RegisterCallback<ClickEvent>(OnContinuePress);
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnDisable()
    {
        menuButton.UnregisterCallback<ClickEvent>(OnMenuPress);
        retryButton.UnregisterCallback<ClickEvent>(OnRetryPress);
        continueButton.UnregisterCallback<ClickEvent>(OnContinuePress);
    }

    public void Victory()
    {
        stateLabel.text = "Victory!";
        starRow.style.display = DisplayStyle.Flex;
        continueButton.style.display = DisplayStyle.Flex;
        container.style.marginBottom = VictoryMargin;
        container.style.marginLeft = VictoryMargin;
        container.style.marginRight = VictoryMargin;
        container.style.marginTop = VictoryMargin;
        container.style.display = DisplayStyle.Flex;
        playerMovement.enabled = false;
    }

    public void Defeat()
    {
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

    private void OnMenuPress(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnRetryPress(ClickEvent evt)
    {
        //Just fixing the timescale
        playerMovement.ResetTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnContinuePress(ClickEvent evt)
    {
        playerMovement.ResetTimeScale();
        // Load the next scene
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;
        // Check if the next scene index is not out of bounds
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there is no next scene, load the main menu or do something else
            SceneManager.LoadScene("MainMenu");
        }
    }
}
