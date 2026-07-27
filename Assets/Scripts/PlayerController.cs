using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Color boostColor = new Color(0.22f, 0.74f, 0.97f);

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 moveInput;
    private float speedMultiplier = 1f;
    private Color originalColor;
    private Coroutine boostRoutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) originalColor = sprite.color;
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveInput * moveSpeed * speedMultiplier * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public void Boost(float multiplier, float duration)
    {
        if (boostRoutine != null) StopCoroutine(boostRoutine);

        speedMultiplier = multiplier;
        if (sprite != null) sprite.color = boostColor;
        boostRoutine = StartCoroutine(BoostRoutine(duration));
    }

    private IEnumerator BoostRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1f;
        if (sprite != null) sprite.color = originalColor;
        boostRoutine = null;
    }
}