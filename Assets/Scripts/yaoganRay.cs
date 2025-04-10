using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class yaoganRay : MonoBehaviour
{
    //public GameObject testleftTeleportation; // 注意拼写错误，将 "letfTeleportation" 改为 "leftTeleportation"
    public GameObject testrightTeleportation;

    //public InputActionProperty testleftActivate;
    public InputActionProperty testrightActivate;

    // Start is called before the first frame update
    void Start()
    {
        // 确保 InputAction 已正确绑定
        if (testrightActivate.action == null)
        {
            Debug.LogError("InputAction not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 读取摇杆输入值
        //Vector2 leftJoystick = testleftActivate.action.ReadValue<Vector2>();
        Vector2 rightJoystick = testrightActivate.action.ReadValue<Vector2>();

        // 激活左摇杆控制的传送
        //testleftTeleportation.SetActive(leftJoystick.magnitude > 0.1f); // 当摇杆偏移量大于 0.1 时激活

        // 激活右摇杆控制的传送
        testrightTeleportation.SetActive(rightJoystick.magnitude > 0.1f); // 当摇杆偏移量大于 0.1 时激活
    }
}