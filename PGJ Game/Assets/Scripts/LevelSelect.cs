using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{
    public GameObject MainMenu;

    private UIDocument document;
    private Button backButton;

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();
        backButton = document.rootVisualElement.Q("Back") as Button;

        backButton.RegisterCallback<ClickEvent>(OnStartPress);
    }

    private void OnDisable()
    {
        backButton.UnregisterCallback<ClickEvent>(OnStartPress);
    }

    private void OnStartPress(ClickEvent evt)
    {
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
