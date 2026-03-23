using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator sceneAnim;
    private void Awake()
    {
        sceneAnim.Play("Crossfade_Start");
    }
}
