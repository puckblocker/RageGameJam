using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [Header("SFX")]
    public AudioClip background;
    public AudioClip hit;
    public AudioClip laser;
    public AudioClip bullet;
    public AudioClip death;
    public AudioClip gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayerSFX(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
}
