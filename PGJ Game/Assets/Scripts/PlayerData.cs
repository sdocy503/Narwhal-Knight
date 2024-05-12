using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{
    int Score = 0;
    public string ScoreUI = "Score: 0";

    public void SetScore(int score)
    {
        Score = score;
        ScoreUI = "Score: " + Score;
    }
}
