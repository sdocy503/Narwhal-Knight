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
        painter.Arc(new Vector2(width * 0.5f, height * 0.5f), size * 0.5f - 1.25f, Mathf.Lerp(270f, -89f, (m_timeLimit - m_time) / m_timeLimit), 270f);
        painter.ClosePath();
        painter.fillColor = Color.white;
        painter.Fill(FillRule.NonZero);
        painter.Stroke();
    }
}
