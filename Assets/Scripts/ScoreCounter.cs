using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int score;

    public int Score => score;

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"Score: {score}");
    }
}