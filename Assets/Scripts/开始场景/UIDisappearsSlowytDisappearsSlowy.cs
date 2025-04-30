using System.Collections;
using UnityEditor;
using UnityEngine;

public class UIDisappearsSlowy : MonoBehaviour
{
    public CanvasGroup canvasGroup;  // 拖拽你的CanvasGroup组件到这里
    public float fadeSpeed = 2f;     // 渐变速度
    public GameObject GameObject;

    private void Start()
    {
        GameObject.SetActive(false);
        StartCoroutine(FadeTo(0));
    }
    // 显示方法（立即执行）
    public void Show()
    {
        // 如果正在隐藏，先中断当前协程
        GameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeTo(1));
    }
 
    // 隐藏方法（立即执行）
    public void Hide()
    {
        StartCoroutine(FadeAndHide());
    }

    // 等待两秒后隐藏
    private IEnumerator FadeAndHide()
    {
        yield return StartCoroutine(FadeTo(0)); // 先执行淡出
        yield return new WaitForSeconds(fadeSpeed); // 再等待2秒
        GameObject.SetActive(false); // 最后隐藏
    }
    // 通用渐变协程
    private System.Collections.IEnumerator FadeTo(float targetAlpha)
    {
        // 确保CanvasGroup存在
        if (canvasGroup == null) yield break;
 
        // 立即设置初始值（防止协程重复启动时的闪烁）
        canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha);
 
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha,
                targetAlpha,
                Time.deltaTime * fadeSpeed
            );
            yield return null;
        }
 
        // 确保最终值精确
        canvasGroup.alpha = targetAlpha;
    }
}