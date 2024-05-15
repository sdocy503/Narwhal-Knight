using UnityEngine;

public class UITesting : MonoBehaviour {
	public EndScreen endScreen;
	public PlayerData data;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		InvokeRepeating("SwapVic", 4f, 4f);
		InvokeRepeating("SwapDef", 2f, 4f);
	}

	void SwapVic() {
		endScreen.Victory();
		data.MoveCount += 2;
	}

	void SwapDef() {
		endScreen.Defeat();
		data.MoveCount -= 1;
	}
}