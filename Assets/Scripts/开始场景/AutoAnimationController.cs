using UnityEngine;

public class AutoAnimationController : MonoBehaviour
{
    // 自动获取的Animator组件
    private Animator _autoAnimator;

    // 动画触发参数名称
    public string triggerName = "PlayAnimation";

    void Awake()
    {
        // 自动检测当前物体或父级物体的Animator组件
        _autoAnimator = GetComponentInParent<Animator>();

        // 如果未找到Animator，尝试在子物体中查找
        if (_autoAnimator == null)
        {
            _autoAnimator = GetComponentInChildren<Animator>();
        }

        // 错误处理
        if (_autoAnimator == null)
        {
            Debug.LogWarning("未找到Animator组件！", this);
        }
    }

    public void PlayAnimation()
    {
        if (_autoAnimator != null)
        {
            _autoAnimator.SetTrigger(triggerName);
        }
    }
}