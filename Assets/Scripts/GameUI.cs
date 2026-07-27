using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private GameObject lifeIconPrefab;
    [SerializeField] private Transform livesContainer;
    [SerializeField] private Color filledColor = Color.red;
    [SerializeField] private Color emptyColor = Color.black;

    private ScoreCounter scoreCounter;
    private PlayerHealth playerHealth;
    private readonly List<Image> lifeIcons = new List<Image>();

    private void Start()
    {
        scoreCounter = FindAnyObjectByType<ScoreCounter>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();

        int best = PlayerPrefs.GetInt("BestScore", 0);
        bestText.text = $"Best: {best}";

        BuildLifeIcons();
    }

    private void BuildLifeIcons()
    {
        int max = playerHealth != null ? playerHealth.MaxLives : 0;
        for (int i = 0; i < max; i++)
        {
            GameObject go = Instantiate(lifeIconPrefab, livesContainer);
            lifeIcons.Add(go.GetComponent<Image>());
        }
        RefreshLives();
    }

    private void Update()
    {
        if (scoreCounter != null)
            scoreText.text = $"Score: {scoreCounter.Score}";

        RefreshLives();
    }

    private void RefreshLives()
    {
        if (playerHealth == null) return;
        int lives = playerHealth.Lives;
        for (int i = 0; i < lifeIcons.Count; i++)
        {
            lifeIcons[i].color = i < lives ? filledColor : emptyColor;
        }
    }
}