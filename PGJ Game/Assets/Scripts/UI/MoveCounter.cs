using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class MoveCounter : MonoBehaviour
{
    public LevelData levelData;
    public PlayerData playerData;
    private UIDocument doc;
    private Label moveCount;
    private VisualElement movePlate;
    

    readonly Vector3 sizeIncrease = new Vector3(0.15f, 0.15f, 0f);
    Vector3 size;


    void Start()
    {
        doc = GetComponent<UIDocument>();
        moveCount = doc.rootVisualElement.Q<Label>("MoveCount");
        movePlate = doc.rootVisualElement.Q<VisualElement>("MovePlate");
        playerData.MoveCountChanged += UpdateMoveCount;
        moveCount.text = playerData.MoveCount + "/" + levelData.MoveLimit;
        size = movePlate.transform.scale;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            movePlate.style.transitionDuration = new List<TimeValue> { 0.25f };
            movePlate.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOutBounce };
            movePlate.transform.scale = size;
        }
        if(Input.GetMouseButtonDown(0))
        {
            movePlate.style.transitionDuration = new List<TimeValue> { 2.0f };
            movePlate.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOut };
            movePlate.transform.scale = size + sizeIncrease;
        }
    }

    void UpdateMoveCount()
    {
        moveCount.text = playerData.MoveCount + "/" + levelData.MoveLimit;
    }
}
