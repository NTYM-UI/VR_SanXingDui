using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneName : MonoBehaviour
{
    private void Start()
    {
        // 开始场景
        SceneLoader.Instance.SetTargetScene("开始场景");
        SceneLoader.Instance.ChangeScene();
    }
    public void KongBaiScene()
    {
        SceneLoader.Instance.ChangeScene("空白场景");
    }
}
