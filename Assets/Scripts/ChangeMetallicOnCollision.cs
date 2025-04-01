using System.Collections;
using UnityEngine;

public class ChangeMetallicOnCollision : MonoBehaviour
{
    // 设置要改变金属度的目标物体
    public Renderer targetRenderer;
    // 设置新的金属度值（范围是0到1）
    public float newMetallic = 0.3f;

    // 引用 UI Canvas
    public Canvas uiCanvas;

    private void Start()
    {
        // 确保 Canvas 在游戏开始时是隐藏的
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false);
            UnityEngine.Debug.Log("UI Canvas 已隐藏！");
        }
        else
        {
            UnityEngine.Debug.LogError("UI Canvas 未设置！");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("碰撞事件触发！");
        // 当发生碰撞时，检查目标物体是否有效
        if (targetRenderer != null)
        {
            // 启动协程延迟两秒后改变金属度
            StartCoroutine(DelayChangeMetallic(newMetallic));
        }
        else
        {
            UnityEngine.Debug.LogError("目标Renderer未设置！");
        }
    }

    // 协程：延迟两秒后改变金属度
    private IEnumerator DelayChangeMetallic(float metallicValue)
    {
        // 等待两秒
        yield return new WaitForSeconds(2.0f);

        // 获取目标物体的材质
        Material mat = targetRenderer.material;

        // 设置金属度
        mat.SetFloat("_Metallic", metallicValue);
        UnityEngine.Debug.Log("已改变金属度！");

        // 显示 Canvas
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(true); // 确保 Canvas 的 GameObject 被激活
            UnityEngine.Debug.Log("UI Canvas 已激活！");
        }
        else
        {
            UnityEngine.Debug.LogError("UI Canvas 未设置！");
        }
    }

    private void OnDestroy()
    {
        // 停止所有协程
        StopAllCoroutines();
    }
}