using UnityEngine;
using UnityEngine.UIElements;

public class MoveCounter : MonoBehaviour
{
    public LevelData levelData;
    public PlayerData playerData;
    private UIDocument doc;
    private Label moveCount;
    private VisualElement movePlate;


    void Start()
    {
        doc = GetComponent<UIDocument>();
        moveCount = doc.rootVisualElement.Q<Label>("MoveCount");
        movePlate = doc.rootVisualElement.Q<VisualElement>("MovePlate");
        playerData.MoveCountChanged += UpdateMoveCount;
        moveCount.text = playerData.MoveCount + "/" + levelData.MoveLimit;
    }

    void UpdateMoveCount()
    {
        moveCount.text = playerData.MoveCount + "/" + levelData.MoveLimit;
    }
}
