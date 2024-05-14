using UnityEngine;

public class UITesting : MonoBehaviour
{
    public EndScreen endScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SwapVic", 4f, 4f);
        InvokeRepeating("SwapDef", 2f, 4f);
    }

    void SwapVic()
    {
        endScreen.Victory();
    }

    void SwapDef()
    {
        endScreen.Defeat();
    }

}
