using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMetallicOnCollision : MonoBehaviour
{
    // 设置要改变金属度的目标物体数组
    public Renderer[] targetRenderers;
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
        // 检查碰撞物体是否具有特定的 Tag
        if (collision.gameObject.CompareTag("TargetTag")) // 假设目标物体的 Tag 是 "TargetTag"
        {
            // 当发生碰撞时，检查目标物体数组是否有效
            if (targetRenderers != null && targetRenderers.Length > 0)
            {
                // 启动协程延迟两秒后改变金属度
                StartCoroutine(DelayChangeMetallic(newMetallic));
            }
            else
            {
                UnityEngine.Debug.LogError("目标Renderer数组未设置或为空！");
            }
        }
    }

    // 协程：延迟两秒后改变金属度
    private IEnumerator DelayChangeMetallic(float metallicValue)
    {
        // 等待两秒
        yield return new WaitForSeconds(2.0f);

        // 遍历目标物体数组，改变每个物体的金属度
        int count = 0; // 用于计数已改变金属度的物体数量
        foreach (Renderer renderer in targetRenderers)
        {
            if (renderer != null)
            {
                // 获取目标物体的材质
                Material mat = renderer.material;

                // 设置金属度
                mat.SetFloat("_Metallic", metallicValue);
                UnityEngine.Debug.Log("已改变金属度！");

                count++; // 每改变一个物体的金属度，计数加1
            }
            else
            {
                UnityEngine.Debug.LogError("目标Renderer数组中有未设置的元素！");
            }
        }

        // 检查是否所有目标物体的金属度都已改变
        if (count == targetRenderers.Length)
        {
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
    }

    private void OnDestroy()
    {
        // 停止所有协程
        StopAllCoroutines();
    }
}