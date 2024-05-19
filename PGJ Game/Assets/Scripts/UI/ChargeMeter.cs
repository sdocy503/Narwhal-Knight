using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    public PlayerMovement playerMove;
    RectTransform rectTransform;
    Canvas canvas;
    Image image;

    const float BarWidth = 175f;
    const float IncrementChunk = 20f;
    float time = 0f;
    Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        image = GetComponent<Image>();
        rectTransform = transform as RectTransform;
        canvas.enabled = false;
        pos = transform.parent.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.parent.position = playerMove.transform.position;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < playerMove.transform.position.x)
        {
            transform.parent.parent.localRotation = Quaternion.Euler(0, 180, 0);
            //transform.parent.localPosition = Vector3.Scale(pos, new Vector3(-1, 1, 1));
        }
        else
        {
            transform.parent.parent.localRotation = Quaternion.Euler(0, 0, 0);
            //transform.parent.localPosition = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            time = 0f;
            canvas.enabled = false;
        }
        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            canvas.enabled = true;
        }
        time = Mathf.Min(time, playerMove.maxChargeTime);
        float lerpVal = Mathf.Lerp(0f, BarWidth, time / playerMove.maxChargeTime);
        int noRemainder = (int)(lerpVal / IncrementChunk);
        rectTransform.sizeDelta = new Vector2(Mathf.Min(noRemainder * IncrementChunk + (IncrementChunk) * (time / playerMove.maxChargeTime), BarWidth), rectTransform.sizeDelta.y);
    }
}
