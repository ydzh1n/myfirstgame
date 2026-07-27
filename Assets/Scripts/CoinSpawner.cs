using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int desiredCount = 3;
    [SerializeField] private float respawnDelay = 0.5f;
    [SerializeField] private float spawnRadius = 0.9f;
    [SerializeField] private int maxAttempts = 20;
    [SerializeField] private float padding = 1f;

    private float spawnMinX = -8f, spawnMaxX = 8f;
    private float spawnMinY = -4f, spawnMaxY = 4f;

    private void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 minWorld = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 maxWorld = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
            spawnMinX = minWorld.x + padding;
            spawnMaxX = maxWorld.x - padding;
            spawnMinY = minWorld.y + padding;
            spawnMaxY = maxWorld.y - padding;
        }

        StartCoroutine(MaintainCoinsRoutine());
    }

    private IEnumerator MaintainCoinsRoutine()
    {
        while (true)
        {
            int current = GameObject.FindGameObjectsWithTag("Coin").Length;
            for (int i = current; i < desiredCount; i++)
            {
                if (TryFindValidPoint(out Vector2 point))
                    Instantiate(coinPrefab, point, Quaternion.identity);
                else
                    break;
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private bool TryFindValidPoint(out Vector2 point)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector2 candidate = new Vector2(
                Random.Range(spawnMinX, spawnMaxX),
                Random.Range(spawnMinY, spawnMaxY));
            if (IsPointValid(candidate))
            {
                point = candidate;
                return true;
            }
        }
        point = default;
        return false;
    }

    private bool IsPointValid(Vector2 point)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(point, spawnRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<DamageZone>() != null) return false;
            if (hit.GetComponent<PlayerHealth>() != null) return false;
            if (hit.CompareTag("Coin")) return false;
        }
        return true;
    }
}