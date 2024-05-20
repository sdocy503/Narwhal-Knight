using UnityEngine;

public class UITesting : MonoBehaviour {
	public EndScreen endScreen;
	public PlayerData data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SwapVic", 6f, 8f);
        InvokeRepeating("SwapDef", 2f, 8f);
    }


	void SwapVic() {
		endScreen.Victory(2);
		data.MoveCount += 2;
	}

	void SwapDef() {
		endScreen.Defeat();
		data.MoveCount -= 1;
	}
}