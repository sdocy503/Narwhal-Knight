using Unity.VisualScripting;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float bob = 0.2f;
    public float bobTime = 8f;
    public AnimationCurve interpolationStop;
    public AnimationCurve interpolationStart;
    float offset = 0f;
    float time = 0f;
    float yPos;
    float xPos;

    private void Start()
    {
        yPos = transform.localPosition.y;
        xPos = transform.localPosition.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        

        if (time > bobTime)
            time -= bobTime;
        if(time < bobTime * 0.25f)
            offset = Mathf.Lerp(yPos, yPos + bob, interpolationStop.Evaluate(time / (bobTime * 0.25f)));
        else if (time < bobTime * 0.5f)
            offset = Mathf.Lerp(yPos + bob, yPos, interpolationStart.Evaluate((time - (bobTime * 0.25f)) / (bobTime * 0.25f)));
        else if(time < bobTime * 0.75f)
            offset = Mathf.Lerp(yPos, yPos - bob, interpolationStop.Evaluate((time - (bobTime * 0.5f)) / (bobTime * 0.25f)));
        else
            offset = Mathf.Lerp(yPos - bob, yPos, interpolationStart.Evaluate((time - (bobTime * 0.75f)) / (bobTime * 0.25f)));

        transform.localPosition = transform.InverseTransformPoint(transform.TransformPoint(new Vector3(xPos, yPos)) + new Vector3(0, offset, 0));
    }
}
