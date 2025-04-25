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
    //public Canvas uiCanvas;

    // 引用组合后的完整物体
    public GameObject combinedObject;

    // 悬浮效果的参数
    public float floatTargetHeight = 0.3f; // 悬浮目标高度
    public float floatSpeed = 0.5f; // 悬浮速度

    // 音效相关
    public AudioClip sweepSound; // 播放的音效
    private AudioSource audioSource; // 音频源

    private Vector3 originalPosition; // 记录组合物体的原始位置
    private bool isFloating = false; // 是否正在悬浮
    private bool hasReachedTargetHeight = false; // 是否已达到目标高度

    private void Start()
    {
        // 确保 Canvas 在游戏开始时是隐藏的
        /*if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false);
            UnityEngine.Debug.Log("UI Canvas 已隐藏！");
        }
        else
        {
            UnityEngine.Debug.LogError("UI Canvas 未设置！");
        }*/

        // 确保组合后的完整物体在游戏开始时是隐藏的
        if (combinedObject != null)
        {
            combinedObject.SetActive(false);
            UnityEngine.Debug.Log("组合后的完整物体已隐藏！");
        }
        else
        {
            UnityEngine.Debug.LogError("组合后的完整物体未设置！");
        }

        // 初始化音效
        audioSource = gameObject.AddComponent<AudioSource>(); // 添加 AudioSource 组件
        audioSource.clip = sweepSound; // 设置音效
        audioSource.playOnAwake = false; // 不在启动时自动播放
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
                // 播放音效
                if (sweepSound != null)
                {
                    audioSource.Play();
                }
                else
                {
                    UnityEngine.Debug.LogError("未设置音效文件！");
                }

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
            /*if (uiCanvas != null)
            {
                uiCanvas.gameObject.SetActive(true); // 确保 Canvas 的 GameObject 被激活
                UnityEngine.Debug.Log("UI Canvas 已激活！");
            }
            else
            {
                UnityEngine.Debug.LogError("UI Canvas 未设置！");
            }*/

            // 显示组合后的完整物体
            if (combinedObject != null)
            {
                combinedObject.SetActive(true);
                originalPosition = combinedObject.transform.position; // 记录原始位置
                isFloating = true; // 开始悬浮效果
                UnityEngine.Debug.Log("组合后的完整物体已显示！");
            }
            else
            {
                UnityEngine.Debug.LogError("组合后的完整物体未设置！");
            }

            // 隐藏所有目标物体
            foreach (Renderer renderer in targetRenderers)
            {
                if (renderer != null)
                {
                    renderer.gameObject.SetActive(false);
                }
            }

            // 在悬浮效果完成后切换场景
            StartCoroutine(SwitchSceneAfterFloating());
        }
    }

    private IEnumerator SwitchSceneAfterFloating()
    {
        // 等待悬浮效果完成
        while (!hasReachedTargetHeight)
        {
            yield return null;
        }

        // 悬浮效果完成后切换场景
        SceneLoader.Instance.ChangeScene("视频介绍"); // 替换为你的目标场景名称
    }

    private void Update()
    {
        // 实现悬浮效果
        if (isFloating && combinedObject != null)
        {
            if (!hasReachedTargetHeight)
            {
                // 计算目标位置
                Vector3 targetPosition = originalPosition + new Vector3(0, floatTargetHeight, 0);

                // 平滑移动到目标位置
                combinedObject.transform.position = Vector3.MoveTowards(combinedObject.transform.position, targetPosition, floatSpeed * Time.deltaTime);

                // 检查是否已到达目标高度
                if (Vector3.Distance(combinedObject.transform.position, targetPosition) < 0.01f)
                {
                    hasReachedTargetHeight = true;
                    combinedObject.transform.position = targetPosition; // 确保精确到达目标位置
                }
            }
        }
        // 检查是否需要停止音效
        if (!isFloating && audioSource.isPlaying)
        {
            audioSource.Stop(); // 停止音效
            UnityEngine.Debug.Log("音效已停止！");
        }
    }

    private void OnDestroy()
    {
        // 停止所有协程
        StopAllCoroutines();
    }
}