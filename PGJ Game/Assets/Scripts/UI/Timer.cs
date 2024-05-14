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

    private void Start()
    {
        doc = GetComponent<UIDocument>();
        comp = doc.rootVisualElement.Q<TimerComponent>();
        comp.dataSource = this;
        comp = doc.rootVisualElement.Q<Label>("Time");
        comp.dataSource = this;
        timeLimit = data.TimeLimit;
    }
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        timeInt = (int)(timeLimit - time);
    }
}
