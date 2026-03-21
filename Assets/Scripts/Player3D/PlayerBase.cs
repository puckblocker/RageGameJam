using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Variables
    [field: SerializeField] public float speed { get; set; }    // use of properties for getter and setter (prevents need for the functions I usually do)
    [field: SerializeField] public static int rageCnt {  get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
