using UnityEngine;

public class GyroscopeController : MonoBehaviour
{
    [SerializeField] private float deadzone = 0.05f;
    [SerializeField] private float maxTilt = 0.5f;

    private bool available;
    private Quaternion baseAttitude;

    public bool IsAvailable => available;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            baseAttitude = Input.gyro.attitude;
            available = true;
        }
    }

    public Vector2 GetInput()
    {
        if (!available) return Vector2.zero;

        Quaternion relative = Quaternion.Inverse(baseAttitude) * Input.gyro.attitude;
        Vector3 tilt = relative * Vector3.forward;
        Vector2 raw = new Vector2(tilt.x, tilt.y);

        if (raw.magnitude < deadzone) return Vector2.zero;

        return Vector2.ClampMagnitude(raw / maxTilt, 1f);
    }
}