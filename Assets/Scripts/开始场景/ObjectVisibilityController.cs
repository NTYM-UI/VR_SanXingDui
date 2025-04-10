using UnityEngine;

public class ObjectVisibilityController : MonoBehaviour
{
    // 需要控制的两个物体
    public GameObject object1;
    public GameObject object2;

    // 动画开始时的触发方法
    public void OnAnimationStart()
    {
        object1.SetActive(false);
        object2.SetActive(false);
    }

    // 动画结束时的触发方法
    public void OnAnimationEnd()
    {
        object1.SetActive(true);
        object2.SetActive(true);
    }
}