using UnityEngine;

public class ChargeMeter : MonoBehaviour
{
    public PlayerMovement playerMove;
    RectTransform rectTransform;

    const float BarWidth = 175f;
    const float IncrementChunk = 20f;
    float time = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            time = 0f;
        if (Input.GetMouseButton(0))
            time += Time.deltaTime;
        time = Mathf.Min(time, playerMove.maxChargeTime);
        float lerpVal = Mathf.Lerp(0f, BarWidth, time / playerMove.maxChargeTime);
        int noRemainder = (int)(lerpVal / IncrementChunk);
        rectTransform.sizeDelta = new Vector2(Mathf.Min(noRemainder * IncrementChunk + (IncrementChunk) * (time / playerMove.maxChargeTime), BarWidth), rectTransform.sizeDelta.y);
    }
}
