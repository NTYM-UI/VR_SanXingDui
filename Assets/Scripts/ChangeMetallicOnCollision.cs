using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMetallicOnCollision : MonoBehaviour
{
    // 设置要改变金属度的目标物体数组
    public Renderer[] targetRenderers;
    // 设置新的金属度值（范围是0到1）
    public float newMetallic = 0.3f;

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

    // 用于记录每个物体的清扫状态
    private bool[] cleanedStates;

    private void Start()
    {
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

        // 检查音效是否正确设置
        if (sweepSound == null)
        {
            UnityEngine.Debug.LogError("未设置音效文件！");
        }
        else
        {
            UnityEngine.Debug.Log("音效文件已正确设置！");
        }

        // 初始化清扫状态数组
        cleanedStates = new bool[targetRenderers.Length];
        for (int i = 0; i < cleanedStates.Length; i++)
        {
            cleanedStates[i] = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("碰撞事件触发！");
        // 检查碰撞物体是否是目标物体之一
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            if (collision.gameObject == targetRenderers[i].gameObject)
            {
                // 如果该物体尚未被清扫，则标记为已清扫
                if (!cleanedStates[i])
                {
                    cleanedStates[i] = true;
                    UnityEngine.Debug.Log($"已清扫物体 {i + 1}");

                    // 播放音效
                    if (sweepSound != null)
                    {
                        AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
                        tempAudioSource.clip = sweepSound;
                        tempAudioSource.playOnAwake = false;
                        tempAudioSource.Play();

                        // 启动协程，确保音效播放1秒后停止
                        StartCoroutine(StopAudioAfterDelay(tempAudioSource, 1.0f));

                        UnityEngine.Debug.Log("播放音效！");
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("未设置音效文件！");
                    }

                    // 启动协程改变该物体的金属度
                    StartCoroutine(ChangeMetallicForRenderer(targetRenderers[i], newMetallic));
                }

                // 检查是否所有物体都已清扫
                if (AllCleaned())
                {
                    UnityEngine.Debug.Log("所有目标物体都已清扫！");
                    ShowCombinedObject();
                }

                break; // 退出循环
            }
        }
    }

    // 协程：在指定延迟后停止音效
    private IEnumerator StopAudioAfterDelay(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null)
        {
            audioSource.Stop();
            UnityEngine.Debug.Log("音效已停止！");
        }
    }

    // 协程：改变指定 Renderer 的金属度
    private IEnumerator ChangeMetallicForRenderer(Renderer renderer, float metallicValue)
    {
        // 等待音效播放完成（假设音效长度为 1 秒）
        yield return new WaitForSeconds(1.0f);

        // 获取目标物体的材质
        Material mat = renderer.material;

        // 设置金属度
        mat.SetFloat("_Metallic", metallicValue);
        UnityEngine.Debug.Log("已改变金属度！");
    }

    // 检查是否所有物体都已清扫
    private bool AllCleaned()
    {
        foreach (bool cleaned in cleanedStates)
        {
            if (!cleaned)
            {
                return false; // 如果有任何物体未清扫，返回 false
            }
        }
        return true; // 所有物体都已清扫
    }

    // 显示组合后的完整物体并启动悬浮效果
    private void ShowCombinedObject()
    {
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

    private IEnumerator SwitchSceneAfterFloating()
    {
        // 等待悬浮效果完成
        while (!hasReachedTargetHeight)
        {
            yield return null;
        }

        // 悬浮效果完成后切换场景
        UnityEngine.Debug.Log("悬浮效果完成，准备切换场景！");
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
                    UnityEngine.Debug.Log("悬浮效果完成！");
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