using System.Collections;
using UnityEngine;

public class ObjectDisappearsSlowy1 : MonoBehaviour
{
    public GameObject objectName1; // 驼峰命名法
    public float fadeDurationAppear1 = 2f;
    public float fadeDurationDisappear1 = 2f;
    private Material materialInstance1;
    private Renderer objectRenderer1; // 缓存Renderer引用

    void Start()
    { 
        // 初始化
        if (objectName1 != null)
        {
            objectRenderer1 = objectName1.GetComponent<Renderer>();
            if (objectRenderer1 != null)
            {
                materialInstance1 = new Material(objectRenderer1.material);
                objectRenderer1.material = materialInstance1;
            }
            objectName1.SetActive(true);
            //ProjectNameAppear();
        }
        else
        {
            Debug.LogError("ChuanSongMen GameObject is not assigned!", this);
        }
    }

    // 显示物体
    public void objectAppear1()
    {
        if (objectName1 != null)
        {
            objectName1.SetActive(true);
            StartCoroutine(FadeAppear1());
        }
    }

    // 隐藏物体
    public void objectDisappear1()
    {
        if (objectName1 != null)
        {
            StartCoroutine(FadeAndDisappear1());
        }
    }

    // 组合淡出+隐藏的协程
    private IEnumerator FadeAndDisappear1()
    {
        yield return StartCoroutine(FadeDisappear1()); // 先执行淡出
        yield return new WaitForSeconds(fadeDurationDisappear1); // 再等待2秒
        objectName1.SetActive(false); // 最后隐藏
    }

    // 淡入协程
    private IEnumerator FadeAppear1()
    {
        if (objectRenderer1 == null || materialInstance1 == null) yield break;

        float elapsedTime = 0f;
        Color startColor = new Color(1, 1, 1, 0); // 完全透明
        Color endColor = Color.white; // 完全不透明

        while (elapsedTime < fadeDurationAppear1)
        {
            materialInstance1.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDurationAppear1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        materialInstance1.color = endColor;
    }

    // 淡出协程
    private IEnumerator FadeDisappear1()
    {
        if (objectRenderer1 == null || materialInstance1 == null) yield break;

        float elapsedTime = 0f;
        Color startColor = Color.white; // 完全不透明
        Color endColor = new Color(1, 1, 1, 0); // 完全透明

        while (elapsedTime < fadeDurationDisappear1)
        {
            materialInstance1.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDurationDisappear1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        materialInstance1.color = endColor;
    }
}