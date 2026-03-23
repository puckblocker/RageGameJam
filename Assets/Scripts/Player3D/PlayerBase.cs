using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Variables
    [field: SerializeField] public float speed { get; set; }    // use of properties for getter and setter (prevents need for the functions I usually do)
    [field: SerializeField] public int health { get; set; }

    private static int score = 0;

    public static int scoreVal
    {
        get { return score; }
        set { score = value; }
    }

    private static int rage = 0;
    public static int rageCnt
    {
        get { return rage; }
        set { rage = value; }
    }
}
