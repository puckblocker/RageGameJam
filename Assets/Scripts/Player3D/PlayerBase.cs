using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Variables
    [field: SerializeField] public float speed { get; set; }    // use of properties for getter and setter (prevents need for the functions I usually do)
    [field: SerializeField] public int rageCnt {  get; set; }
    private static int health = 3;

    public static int healthVal
    {
        get { return health; }
        set { health = value; }
    }

    private static int score = 0;

    public static int scoreVal
    {
        get { return score; }
        set { score = value; }
    }
}
