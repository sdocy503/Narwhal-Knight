using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class TimerComponent : VisualElement
{
    [SerializeField, DontCreateProperty]
    float m_time = 0f;

    [UxmlAttribute, CreateProperty]
    public float time
    {
        get => m_time;
        set
        {
            m_time = Mathf.Clamp(value, 0f, m_timeLimit);
            MarkDirtyRepaint(); 
        }
    }

    [UxmlAttribute, CreateProperty]
    public AnimationCurve greenToYellow;

    [UxmlAttribute, CreateProperty]
    public AnimationCurve yellowToRed;

    [UxmlAttribute, CreateProperty]
    public Color green;

    [UxmlAttribute, CreateProperty]
    public Color yellow;

    [UxmlAttribute, CreateProperty]
    public Color red;

    [SerializeField, DontCreateProperty]
    float m_timeLimit = 1f;

    [UxmlAttribute, CreateProperty]
    public float timeLimit
    {
        get => m_timeLimit;
        set
        {
            m_timeLimit = value;
            MarkDirtyRepaint();
        }
    }

    public TimerComponent()
    {
        generateVisualContent += GenerateVisualContent;
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        float width = contentRect.width;
        float height = contentRect.height;

        var painter = context.painter2D;
        float size = Mathf.Min(width, height);
        /*painter.BeginPath();
        painter.lineWidth = 10f;
        painter.Arc(new Vector2(width * 0.5f, height * 0.5f), size * 0.5f, 0f, 360f);
        painter.ClosePath();
        painter.Stroke();*/

        painter.BeginPath();
        painter.lineWidth = 7.5f;
        painter.LineTo(new Vector2(width * 0.5f, height * 0.5f));
        float time = (m_timeLimit - m_time) / m_timeLimit;
        painter.Arc(new Vector2(width * 0.5f, height * 0.5f), size * 0.5f - 1.25f, Mathf.Lerp(270f, -89f, time), 270f);
        painter.ClosePath();
        if(time > 0.5f)
            painter.fillColor = Color.Lerp(yellow, green, greenToYellow.Evaluate(time));
        else
            painter.fillColor = Color.Lerp(red, yellow, yellowToRed.Evaluate(time));
        painter.Fill(FillRule.NonZero);
        painter.Stroke();
    }
}
