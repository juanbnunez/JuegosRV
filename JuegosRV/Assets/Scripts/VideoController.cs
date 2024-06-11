using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public List<string> videoNames;
    private int currentVideoIndex;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoNames = new List<string> { "Casa", "Cola", "Lima", "Lobo", "Luna", "Mami", "Masa", "Mono", "Nena", "Sala" };
        currentVideoIndex = 0;
        LoadVideo(currentVideoIndex);
    }

    public void PlayCurrentVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
        videoPlayer.Play();
    }

    public void PlayNextVideo()
    {
        currentVideoIndex = (currentVideoIndex + 1) % videoNames.Count;
        LoadVideo(currentVideoIndex);
        videoPlayer.Play();
    }

    private void LoadVideo(int index)
    {
        string videoPath = "Videos/" + videoNames[index];
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoPath + ".mp4");
    }
}
