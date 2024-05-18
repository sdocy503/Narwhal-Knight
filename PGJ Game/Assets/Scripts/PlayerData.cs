using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject {
	public delegate void _MoveCountChanged();

	public _MoveCountChanged MoveCountChanged;
	public int Score = 0;
	public string ScoreUI = "Score: 0";
	public int MoveCount
	{
		get => moveCount;
		set
		{
			moveCount = value;
			if(MoveCountChanged != null)
				MoveCountChanged();
        }
	}

	private int moveCount = 0;

	public void SetScore(int score) {
		Score = score;
		ScoreUI = "Score: " + Score;
	}
}