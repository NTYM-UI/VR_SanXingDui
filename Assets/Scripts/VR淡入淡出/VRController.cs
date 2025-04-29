using Unity.XR.PXR;
using UnityEngine;

public class VRController : MonoBehaviour
{
    public PXR_ScreenFade fadeController; // 拖拽赋值

    [Header("淡入淡出时间设置")]
    [Tooltip("淡出持续时间（秒）")]
    public float fadeOutDuration = 2f;

    [Tooltip("淡入持续时间（秒）")]
    public float fadeInDuration = 3f;
    public void StartVRScene()
    {
        // 开始淡出（持续2秒）
        fadeController.StartFadeOut(fadeOutDuration);
    }

    public void ExitVRScene()
    {
        // 开始淡入（持续3秒）
        fadeController.StartFadeIn(fadeInDuration);
    }
}