using UnityEngine;

public class PhoneBase : MonoBehaviour
{
    // Inspector Components
    [SerializeField] private Transform phoneTransform;
    [SerializeField] private GameObject phoneObj;
    [SerializeField] private Rigidbody phoneRigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private float throwForce = 10.0f;
    [SerializeField] private float upwardArc = 5.0f;
    [SerializeField] private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ThrowPhone()
    {
        animator.Play("Throw");
    }

    // Triggers During Animation Event
    public void ReleasePhone()
    {
        Debug.Log("Released Phone");

        // Release Phone From Parent
        phoneObj.transform.parent = null;
        //phoneRigidbody.useGravity = true;
        phoneRigidbody.isKinematic = false;

        // Throw Phone With Force
        Vector3 forwardForce = cam.transform.forward * throwForce;
        Vector3 upForce = Vector3.up * upwardArc;
        Vector3 finalForce = forwardForce + upForce;
        phoneRigidbody.AddForce(finalForce, ForceMode.Impulse);
    }
}
