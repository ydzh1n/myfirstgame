using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float multiplier = 1.5f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            controller.Boost(multiplier, duration);
            Destroy(gameObject);
        }
    }
}