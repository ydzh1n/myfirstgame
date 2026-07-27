using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private TextMeshProUGUI livesText;

    private ScoreCounter scoreCounter;
    private PlayerHealth playerHealth;

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        int best = PlayerPrefs.GetInt("BestScore", 0);
        bestText.text = $"Best: {best}";
    }

    private void Update()
    {
        if (scoreCounter != null)
            scoreText.text = $"Score: {scoreCounter.Score}";

        if (playerHealth != null)
            livesText.text = $"Lives: {playerHealth.Lives}";
    }
}