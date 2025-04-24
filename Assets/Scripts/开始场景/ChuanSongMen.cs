using System.Collections;
using UnityEngine;

public class ChuanSongMen : MonoBehaviour
{
    public GameObject ChuanSongmen;
    public float fadeDuration = 1f;
    private Material materialInstance; // 使用实例化材质避免修改原始材质
    private bool isFading = false;

    void Start()
    {
        // 初始化材质实例
        var renderer = ChuanSongmen.GetComponent<Renderer>();
        if (renderer != null)
        {
            materialInstance = new Material(renderer.material);
            renderer.material = materialInstance;
        }

        ChuanSongMenDisappear();
    }

    public void ChuanSongMenAppear()
    {
        ChuanSongmen.SetActive(true);
        if (isFading || materialInstance == null) return;
        StartCoroutine(FadeAlpha(1f, fadeDuration));
    }

    public void ChuanSongMenDisappear()
    {
        ChuanSongmen.SetActive(false);
    }

    private IEnumerator FadeAlpha(float targetAlpha, float duration)
    {
        isFading = true;
        Color currentColor = materialInstance.color;
        float startAlpha = currentColor.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            materialInstance.color = new Color(
                currentColor.r,
                currentColor.g,
                currentColor.b,
                alpha
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        materialInstance.color = new Color(
            currentColor.r,
            currentColor.g,
            currentColor.b,
            targetAlpha
        );
        isFading = false;
    }
}