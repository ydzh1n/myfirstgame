using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float invulnTime = 1f;

    private int lives;
    private bool invulnerable;
    private ScoreCounter scoreCounter;
    private CameraShake cameraShake;

    public int Lives => lives;
    public int MaxLives => maxLives;

    private void Awake()
    {
        scoreCounter = GetComponent<ScoreCounter>();
        cameraShake = FindAnyObjectByType<CameraShake>();
    }

    private void Start()
    {
        lives = maxLives;
    }

    public void TakeDamage(int amount = 1)
    {
        if (invulnerable) return;

        lives -= amount;
        Debug.Log($"Took damage. Lives left: {lives}");

        if (lives <= 0)
        {
            Die();
            return;
        }

        if (cameraShake != null) cameraShake.Shake();
        StartCoroutine(InvulnerabilityRoutine());
    }

    public bool AddLife(int amount = 1)
    {
        if (lives >= maxLives) return false;

        lives = Mathf.Min(lives + amount, maxLives);
        Debug.Log($"Healed. Lives: {lives}");
        return true;
    }

    private void Die()
    {
        int finalScore = scoreCounter != null ? scoreCounter.Score : 0;
        int best = PlayerPrefs.GetInt("BestScore", 0);

        if (finalScore > best)
        {
            best = finalScore;
            PlayerPrefs.SetInt("BestScore", best);
            PlayerPrefs.Save();
            Debug.Log($"New best score: {best}");
        }
        else
        {
            Debug.Log($"Score: {finalScore}, best: {best}");
        }

        Debug.Log("No lives left. Restarting level.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnTime);
        invulnerable = false;
    }
}