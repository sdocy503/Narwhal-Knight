using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject MainMenu;
    public int NumberOfLevels = 0;

    private List<Button> levelButtons = new List<Button>();
    private UIDocument document;
    private Button backButton;

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();
        backButton = document.rootVisualElement.Q("Back") as Button;

        backButton.RegisterCallback<ClickEvent>(OnStartPress);

        for(int i = 1; i <= NumberOfLevels; i++) 
        {
            Button button = document.rootVisualElement.Q<Button>(i.ToString());
            levelButtons.Add(button);
            button.RegisterCallback<ClickEvent>(OnLevelPress);
        }
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

    private void OnLevelPress(ClickEvent evt)
    {
        string level = evt.target.ToString().Remove(0, 7).Remove(1);
        SceneManager.LoadScene("Level" + level);
    }
}
