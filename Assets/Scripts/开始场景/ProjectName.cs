using System.Collections;
using UnityEngine;

public class ProjectName : MonoBehaviour
{
    public GameObject projectName; // 驼峰命名法
    public float fadeDurationAppear = 2f;
    public float fadeDurationDisappear = 2f;
    private Material materialInstance;
    private Renderer objectRenderer; // 缓存Renderer引用

    void Start()
    {
        // 初始化
        if (projectName != null)
        {
            objectRenderer = projectName.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                materialInstance = new Material(objectRenderer.material);
                objectRenderer.material = materialInstance;
            }
            projectName.SetActive(true);
            //ProjectNameAppear();
        }
        else
        {
            Debug.LogError("ChuanSongMen GameObject is not assigned!", this);
        }
    }

    // 显示传送门
    public void ProjectNameAppear()
    {
        if (projectName != null)
        {
            projectName.SetActive(true);
            StartCoroutine(FadeAppear());
        }
    }

    // 隐藏传送门
    public void ProjectNameDisappear()
    {
        if (projectName != null)
        {
            StartCoroutine(FadeAndDisappear());
        }
    }

    // 组合淡出+隐藏的协程
    private IEnumerator FadeAndDisappear()
    {
        yield return StartCoroutine(FadeDisappear()); // 先执行淡出
        yield return new WaitForSeconds(fadeDurationDisappear); // 再等待2秒
        projectName.SetActive(false); // 最后隐藏
    }

    // 淡入协程
    private IEnumerator FadeAppear()
    {
        if (objectRenderer == null || materialInstance == null) yield break;

        float elapsedTime = 0f;
        Color startColor = new Color(1, 1, 1, 0); // 完全透明
        Color endColor = Color.white; // 完全不透明

        while (elapsedTime < fadeDurationAppear)
        {
            materialInstance.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDurationAppear);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        materialInstance.color = endColor;
    }

    // 淡出协程
    private IEnumerator FadeDisappear()
    {
        if (objectRenderer == null || materialInstance == null) yield break;

        float elapsedTime = 0f;
        Color startColor = Color.white; // 完全不透明
        Color endColor = new Color(1, 1, 1, 0); // 完全透明

        while (elapsedTime < fadeDurationDisappear)
        {
            materialInstance.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDurationDisappear);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        materialInstance.color = endColor;
    }
}