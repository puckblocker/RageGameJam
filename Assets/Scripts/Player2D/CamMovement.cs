using UnityEngine;

public class CamMovement : MonoBehaviour
{
    // Variables
    [SerializeField] Rigidbody2D playerPos;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // Update is called once per frame
    void LateUpdate()
    {
        // Follow Player Within Specifications
        Camera.main.transform.position = new Vector3(Mathf.Clamp(playerPos.position.x, minX, maxX), Mathf.Clamp(playerPos.position.y, minY, maxY), Camera.main.transform.position.z);
    }
}
