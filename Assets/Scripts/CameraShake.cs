using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float magnitude = 0.2f;

    private Vector3 startPos;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        startPos = transform.position;
    }

    public void Shake()
    {
        if (shakeRoutine != null) StopCoroutine(shakeRoutine);
        shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float factor = 1f - elapsed / duration;
            Vector2 offset = Random.insideUnitCircle * magnitude * factor;
            transform.position = startPos + (Vector3)offset;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;
        shakeRoutine = null;
    }
}