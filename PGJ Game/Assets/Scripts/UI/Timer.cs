using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public LevelData data;
    UIDocument doc;
    VisualElement comp;
    TimerComponent timerComp;
    VisualElement timeCircle;

    public float time = 0.0f;
    public float timeLimit = 1.0f;
    private float t = 0.0f;

    private void Start()
    {
        doc = GetComponent<UIDocument>();
        comp = doc.rootVisualElement.Q<Label>("Time");
        comp.dataSource = this;
        timerComp = doc.rootVisualElement.Q<TimerComponent>();
        timerComp.dataSource = this;
        timeCircle = doc.rootVisualElement.Q<VisualElement>("TimeCircle");
        timeLimit = data.TimeLimit;
    }

    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        time = (int)t;
        float scale = (timeLimit - time) / timeLimit;
        if (scale > 0.5f)
            timeCircle.style.unityBackgroundImageTintColor = UnityEngine.Color.Lerp(timerComp.yellow, timerComp.green, timerComp.greenToYellow.Evaluate(scale));
        else
            timeCircle.style.unityBackgroundImageTintColor = UnityEngine.Color.Lerp(timerComp.red, timerComp.yellow, timerComp.yellowToRed.Evaluate(scale));
    }
}
