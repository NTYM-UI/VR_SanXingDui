using UnityEngine;

public class startGame : MonoBehaviour
{
    public GameObject startObject; // 代表开始游戏的物体
    public GameObject settingsObject; // 游戏设置物体

    private PICO4UltraInputHandler inputHandler;

    void Start()
    {
        // 获取PICO4UltraInputHandler脚本的实例
        inputHandler = FindObjectOfType<PICO4UltraInputHandler>();
        if (inputHandler == null)
        {
            Debug.LogError("PICO4UltraInputHandler script not found!");
        }
    }
    
    void Update()
    {
        // 检查左手柄扳机键是否被按下
        if (inputHandler.IsLeftTriggerPressed())
        {
            // 隐藏开始游戏物体和游戏设置物体
            startObject.SetActive(false);
            settingsObject.SetActive(false);
        }
        // 注意：这里只检查了左手柄，你可以根据需要添加对右手柄的检查
    }
}