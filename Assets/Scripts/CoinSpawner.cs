using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int desiredCount = 3;
    [SerializeField] private float respawnDelay = 0.5f;
    [SerializeField] private float safeRadius = 0.5f;
    [SerializeField] private int maxAttempts = 20;
    [SerializeField] private float minX = -8f, maxX = 8f, minY = -4f, maxY = 4f;

    private void Start()
    {
        StartCoroutine(MaintainCoinsRoutine());
    }

    private IEnumerator MaintainCoinsRoutine()
    {
        while (true)
        {
            int current = GameObject.FindGameObjectsWithTag("Coin").Length;
            for (int i = current; i < desiredCount; i++)
            {
                Vector2 point = FindValidPoint();
                Instantiate(coinPrefab, point, Quaternion.identity);
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private Vector2 FindValidPoint()
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector2 point = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            if (IsPointValid(point))
                return point;
        }
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private bool IsPointValid(Vector2 point)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(point, safeRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<DamageZone>() != null) return false;
            if (hit.GetComponent<PlayerHealth>() != null) return false;
            if (hit.CompareTag("Coin")) return false;
        }
        return true;
    }
}