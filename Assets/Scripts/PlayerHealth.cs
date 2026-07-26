using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startLives = 3;
    [SerializeField] private float invulnTime = 1f;

    private int lives;
    private bool invulnerable;

    private void Start()
    {
        lives = startLives;
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

        StartCoroutine(InvulnerabilityRoutine());
    }

    private void Die()
    {
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