using UnityEngine;

public class ObjectVisibilityController : MonoBehaviour
{
    // 使用数组存储需要控制的物体
    public GameObject[] controlledObjects;

    // 动画开始时的触发方法
    public void OnAnimationStart()
    {
        SetObjectsActive(false);
    }

    // 动画结束时的触发方法
    public void OnAnimationEnd()
    {
        SetObjectsActive(true);
    }

    // 统一控制物体激活状态的私有方法
    private void SetObjectsActive(bool isActive)
    {
        foreach (var obj in controlledObjects)
        {
            if (obj != null) // 安全校验
            {
                obj.SetActive(isActive);
            }
        }
    }
}