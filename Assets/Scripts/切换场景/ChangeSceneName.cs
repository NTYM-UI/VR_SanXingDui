using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneName : MonoBehaviour
{
    public void KongBaiScene()
    {
        SceneLoader.Instance.ChangeScene("视频播放和动画场景");
    }
}
