using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            if (health.AddLife(healAmount))
                Destroy(gameObject);
        }
    }
}