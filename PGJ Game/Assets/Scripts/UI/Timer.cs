using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public LevelData data;
    UIDocument doc;
    VisualElement comp;

    public float time = 0.0f;
    public float timeLimit = 1.0f;
    public int timeInt = 0;
    private float t = 0.0f;

    readonly Vector3 sizeIncrease = new Vector3(0.15f, 0.15f, 0f);
    Vector3 size;

    private void Start()
    {
        doc = GetComponent<UIDocument>();
        comp = doc.rootVisualElement.Q<Label>("Time");
        comp.dataSource = this;
        comp = doc.rootVisualElement.Q<TimerComponent>();
        comp.dataSource = this;
        size = comp.transform.scale;
        timeLimit = data.TimeLimit;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            comp.style.transitionDuration = new List<TimeValue> { 0.25f };
            comp.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOutBounce };
            comp.transform.scale = size;
        }
        if (Input.GetMouseButtonDown(0))
        {
            comp.style.transitionDuration = new List<TimeValue> { 2.0f };
            comp.style.transitionTimingFunction = new List<EasingFunction> { EasingMode.EaseOut };
            comp.transform.scale = size + sizeIncrease;
        }
    }

    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        time = (int)t;
        timeInt = (int)(timeLimit - time);
    }
}
