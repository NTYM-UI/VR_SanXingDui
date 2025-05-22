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

    // 引用 Canvas
    public Canvas canvas; // 添加 Canvas 引用

    // 引用烟雾效果预制体
    public GameObject smokePrefab; // 添加烟雾效果预制体

    // 音效相关
    public AudioClip sweepSound; // 播放的音效
    private AudioSource audioSource; // 音频源

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

        // 确保 Canvas 在游戏开始时是隐藏的
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false); // 隐藏 Canvas
            UnityEngine.Debug.Log("Canvas 已隐藏！");
        }
        else
        {
            UnityEngine.Debug.LogError("Canvas 未设置！");
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

    // 显示组合后的完整物体
    private void ShowCombinedObject()
    {
        if (combinedObject != null)
        {
            combinedObject.SetActive(false); // 先隐藏完整物体
            UnityEngine.Debug.Log("组合后的完整物体已隐藏！");

            // 隐藏所有目标物体
            Vector3 centerPosition = Vector3.zero; // 用于存储中心点位置
            int count = 0; // 用于计数有效物体

            foreach (Renderer renderer in targetRenderers)
            {
                if (renderer != null)
                {
                    renderer.gameObject.SetActive(false); // 隐藏目标物体
                    centerPosition += renderer.transform.position; // 累加位置
                    count++;
                }
            }

            // 计算中心点位置
            if (count > 0)
            {
                centerPosition /= count; // 求平均值
            }

            // 显示烟雾效果
            if (smokePrefab != null)
            {
                GameObject smokeInstance = Instantiate(smokePrefab, centerPosition, Quaternion.identity);
                smokeInstance.SetActive(true); // 确保烟雾效果被激活
                UnityEngine.Debug.Log("烟雾效果已实例化并激活！");

                StartCoroutine(ShowSmokeAndCombinedObject(smokeInstance));
            }
            else
            {
                UnityEngine.Debug.LogError("未设置烟雾效果预制体！");
            }
        }
        else
        {
            UnityEngine.Debug.LogError("组合后的完整物体未设置！");
        }
    }

    // 协程：显示烟雾效果并显示完整物体
    private IEnumerator ShowSmokeAndCombinedObject(GameObject smokeInstance)
    {
        // 确保烟雾效果的粒子系统启动
        ParticleSystem[] particleSystems = smokeInstance.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play(); // 启动粒子系统
        }

        yield return new WaitForSeconds(2.0f); // 等待3秒

        // 隐藏烟雾效果
        Destroy(smokeInstance);

        // 显示完整物体
        combinedObject.SetActive(true);
        UnityEngine.Debug.Log("组合后的完整物体已显示！");

        // 显示 Canvas
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true); // 显示 Canvas
            UnityEngine.Debug.Log("Canvas 已显示！");
        }
        else
        {
            UnityEngine.Debug.LogError("Canvas 未设置！");
        }
    }

    private void OnDestroy()
    {
        // 停止所有协程
        StopAllCoroutines();
    }
}