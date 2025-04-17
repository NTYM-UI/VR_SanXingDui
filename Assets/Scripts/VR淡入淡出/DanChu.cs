using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class DanChu : MonoBehaviour
{
    public PXR_ScreenFade fadeController; // 拖拽赋值

    [Header("淡入淡出时间设置")]
    [Tooltip("淡出持续时间（秒）")]
    public float fadeOutDuration = 3f;
    void Start()
    {
        // 开始淡出（持续2秒）
        fadeController.StartFadeOut(fadeOutDuration);
    }
}
