using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 单例模式实现
    private static SceneLoader _instance;
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneLoader>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SceneLoader");
                    _instance = obj.AddComponent<SceneLoader>();
                }
            }
            return _instance;
        }
    }

    [Header("场景切换设置")]
    private string _targetSceneName;

    // 添加场景名称设置方法
    public void SetTargetScene(string sceneName)
    {
        _targetSceneName = sceneName;
        Debug.Log($"目标场景已设置为: {sceneName}");
    }

    // 修改切换方法支持直接传参
    public void ChangeScene(string sceneName = null)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            _targetSceneName = sceneName;
        }

        if (!string.IsNullOrEmpty(_targetSceneName))
        {
            SceneManager.LoadScene(_targetSceneName);
        }
        else
        {
            Debug.LogWarning("请先设置目标场景名称");
        }
    }

    // 确保单例唯一性
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景持久化
        }
    }
}

