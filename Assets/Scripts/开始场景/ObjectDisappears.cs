using UnityEngine;

public class ObjectDisappears : MonoBehaviour
{
    // 声明一个公共数组用于存储需要控制的物体
    public GameObject[] targetObjects;

    // 按钮点击时触发的函数
    public void HideObjects()
    {
        // 遍历数组中的每个物体
        foreach (GameObject obj in targetObjects)
        {
            // 关闭物体（隐藏+禁用碰撞）
            obj.SetActive(false);
        }
    }
}