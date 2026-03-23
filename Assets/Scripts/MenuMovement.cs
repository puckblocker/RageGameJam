using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    private float amplitude = 0.5f;
    private float frequency = 1.0f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = startPos + new Vector3(0, offset, 0);
    }
}
