using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ScoreCounter>(out ScoreCounter counter))
        {
            counter.AddScore(value);
            Destroy(gameObject);
        }
    }
}