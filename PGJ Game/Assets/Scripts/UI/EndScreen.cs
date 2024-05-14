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
    }

    private void OnMenuPress(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnRetryPress(ClickEvent evt)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnContinuePress(ClickEvent evt)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
