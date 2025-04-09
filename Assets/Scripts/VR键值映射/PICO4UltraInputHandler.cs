using UnityEngine;
using UnityEngine.XR;

public class PICO4UltraInputHandler : MonoBehaviour
{
    // 获取左右手柄的 InputDevice
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;

    // 存储左右手柄按键状态
    private bool leftTriggerPressed = false;    // 扳机键
    private bool rightTriggerPressed = false;
    private bool leftGripPressed = false;   // 抓握键
    private bool rightGripPressed = false;
    private bool leftJoystickClicked = false;   // 摇杆点击
    private bool rightJoystickClicked = false;
    private bool leftXButtonPressed = false;    // X/A 键
    private bool rightXButtonPressed = false;
    private bool leftYButtonPressed = false;    // Y/B 键
    private bool rightYButtonPressed = false;

    void Start()
    {
        leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (leftHandDevice.isValid && rightHandDevice.isValid)
        {
            Debug.Log("Both hand devices are valid.");
        }
        else
        {
            Debug.LogError("One or both hand devices are invalid.");
        }
    }

    void Update()
    {
        // 处理左手柄输入
        HandleHandInput(leftHandDevice, "Left");

        // 处理右手柄输入
        HandleHandInput(rightHandDevice, "Right");
    }

    private void HandleHandInput(InputDevice device, string handName)
    {
        // 扳机键
        bool triggerPressed;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            Debug.Log($"{handName} trigger button is pressed.");
            if (device == leftHandDevice)
            {
                leftTriggerPressed = true;
            }
            else if (device == rightHandDevice)
            {
                rightTriggerPressed = true;
            }
        }
        else
        {
            if (device == leftHandDevice)
            {
                leftTriggerPressed = false;
            }
            else if (device == rightHandDevice)
            {
                rightTriggerPressed = false;
            }
        }

        // 抓握键
        bool gripPressed;
        if (device.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed) && gripPressed)
        {
            Debug.Log($"{handName} grip button is pressed.");
            if (device == leftHandDevice)
            {
                leftGripPressed = true;
            }
            else if (device == rightHandDevice)
            {
                rightGripPressed = true;
            }
        }
        else
        {
            if (device == leftHandDevice)
            {
                leftGripPressed = false;
            }
            else if (device == rightHandDevice)
            {
                rightGripPressed = false;
            }
        }

        // 摇杆点击
        bool joystickClicked;
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out joystickClicked) && joystickClicked)
        {
            Debug.Log($"{handName} joystick is clicked.");
            if (device == leftHandDevice)
            {
                leftJoystickClicked = true;
            }
            else if (device == rightHandDevice)
            {
                rightJoystickClicked = true;
            }
        }
        else
        {
            if (device == leftHandDevice)
            {
                leftJoystickClicked = false;
            }
            else if (device == rightHandDevice)
            {
                rightJoystickClicked = false;
            }
        }

        // 摇杆方向
        Vector2 joystickDirection;
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickDirection))
        {
            Debug.Log($"{handName} joystick direction: ({joystickDirection.x}, {joystickDirection.y})");
        }

        // X/A 键
        bool xButtonPressed;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out xButtonPressed) && xButtonPressed)
        {
            Debug.Log($"{handName} X/A button is pressed.");
            if (device == leftHandDevice)
            {
                leftXButtonPressed = true;
            }
            else if (device == rightHandDevice)
            {
                rightXButtonPressed = true;
            }
        }
        else
        {
            if (device == leftHandDevice)
            {
                leftXButtonPressed = false;
            }
            else if (device == rightHandDevice)
            {
                rightXButtonPressed = false;
            }
        }

        // Y/B 键
        bool yButtonPressed;
        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out yButtonPressed) && yButtonPressed)
        {
            Debug.Log($"{handName} Y/B button is pressed.");
            if (device == leftHandDevice)
            {
                leftYButtonPressed = true;
            }
            else if (device == rightHandDevice)
            {
                rightYButtonPressed = true;
            }
        }
        else
        {
            if (device == leftHandDevice)
            {
                leftYButtonPressed = false;
            }
            else if (device == rightHandDevice)
            {
                rightYButtonPressed = false;
            }
        }
    }

    // 提供外部访问左右手柄按键状态的方法
    // 扳机键
    public bool IsLeftTriggerPressed()
    {
        return leftTriggerPressed;
    }

    public bool IsRightTriggerPressed()
    {
        return rightTriggerPressed;
    }

    // 抓握键
    public bool IsLeftGripPressed()
    {
        return leftGripPressed;
    }

    public bool IsRightGripPressed()
    {
        return rightGripPressed;
    }

    //摇杆点击
    public bool IsLeftJoystickClicked()
    {
        return leftJoystickClicked;
    }

    public bool IsRightJoystickClicked()
    {
        return rightJoystickClicked;
    }

    // X/A 键
    public bool IsLeftXButtonPressed()
    {
        return leftXButtonPressed;
    }

    public bool IsRightXButtonPressed()
    {
        return rightXButtonPressed;
    }

    // Y/B 键
    public bool IsLeftYButtonPressed()
    {
        return leftYButtonPressed;
    }

    public bool IsRightYButtonPressed()
    {
        return rightYButtonPressed;
    }
}