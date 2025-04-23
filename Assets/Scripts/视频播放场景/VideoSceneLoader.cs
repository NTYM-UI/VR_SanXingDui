using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;
using Unity.XR.PXR;

public class VideoSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "开头动画场景";
    public PXR_ScreenFade fadeController;
    public float fadeOutDuration = 3f;

    void Start()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        // 先开始淡出
        fadeController.StartFadeOut(fadeOutDuration);

        // 等待淡出完成
        yield return new WaitForSeconds(fadeOutDuration);

        // 加载视频（假设视频需要播放）
        videoPlayer.Play();

        // 等待视频播放结束
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // 开始淡入
        fadeController.StartFadeIn(fadeOutDuration);

        // 等待淡入完成再加载场景
        yield return new WaitForSeconds(fadeOutDuration);

        SceneManager.LoadScene(nextSceneName);
    }
}