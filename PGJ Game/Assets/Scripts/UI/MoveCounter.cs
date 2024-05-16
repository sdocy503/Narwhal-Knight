using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCounter : MonoBehaviour
{
    public LevelData levelData;
    public PlayerData playerData;
    private UIDocument doc;
    private Label moveCount;
    private VisualElement movePlate;
    private TimerComponent timer;
    private VisualElement container;

    readonly Vector3 sizeIncrease = new Vector3(0.15f, 0.15f, 0f);
    Vector3 size;
    Vector3 movePos;


    void Start()
    {
        doc = GetComponent<UIDocument>();
        moveCount = doc.rootVisualElement.Q<Label>("MoveCount");
        movePlate = doc.rootVisualElement.Q<VisualElement>("MovePlate");
        timer = doc.rootVisualElement.Q<TimerComponent>();
        container = doc.rootVisualElement.Q<VisualElement>("Container");

        playerData.MoveCountChanged += UpdateMoveCount;
        moveCount.text = playerData.MoveCount + "/" + levelData.MoveLimit;
        movePos = container.transform.position;
        size = movePlate.transform.scale;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            movePlate.style.transitionDuration = new List<TimeValue> { 0.25f };
            movePlate.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOutBounce };
            timer.style.transitionDuration = new List<TimeValue> { 0.25f };
            timer.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOutBounce };
            timer.transform.scale = size;
            movePlate.transform.scale = size;
            InvokeRepeating("UIShake", 0f, 0.01f);
            Invoke("EndShake", 0.3f);
        }
        if(Input.GetMouseButtonDown(0))
        {
            movePlate.style.transitionDuration = new List<TimeValue> { 2.0f };
            movePlate.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOut };
            timer.style.transitionDuration = new List<TimeValue> { 2.0f };
            timer.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOut };
            timer.transform.scale = size + sizeIncrease;
            movePlate.transform.scale = size + sizeIncrease;
        }
    }

    void UIShake()
    {
        Vector3 rand = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        container.transform.position = movePos + rand * 2.5f;
    }

    void EndShake()
    {
        CancelInvoke("UIShake");
        container.transform.position = movePos;
    }

    public void UpdateMoveCount()
    {
        moveCount.text = playerData.MoveCount + "/" + levelData.MoveLimit;
    }
}
