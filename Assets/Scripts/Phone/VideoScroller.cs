using UnityEngine;
using UnityEngine.Video;

public class VideoScroller : MonoBehaviour
{
    // Video Components
    [SerializeField] private VideoPlayer video;
    [SerializeField] private VideoPlayer video2;
    [SerializeField] private VideoClip video3;
    [SerializeField] private VideoClip video4;
    int cntr = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    // Update is called once per frame
    public void switchVideo()
    {
        //video.enabled = !video.enabled;
        //video2.enabled = !video2.enabled;
        if(cntr == 0)
        {
            cntr++;
            video.clip = video4;
        }
        else
        {
            cntr--;
            video.clip = video3;
        }

    }
}
