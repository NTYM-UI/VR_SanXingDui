using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAndDisappear : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Material materialInstance; // 使用实例化材质避免修改原始材质
    private bool isFading = false;

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
