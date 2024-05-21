using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportOnButtonPressTwo : MonoBehaviour
{
    public XRNode inputSource;
    private InputDevice device;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    void Update()
    {
        bool buttonA = false;
        bool buttonB = false;
        bool buttonX = false;
        bool buttonY = false;

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonA) && buttonA)
        {
            TeleportTo(new Vector3(0, 0, 10));
        }

        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonB) && buttonB)
        {
            TeleportTo(new Vector3(0, 0, -10));
        }

        if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out buttonX) && buttonX)
        {
            TeleportTo(new Vector3(10, 0, 0));
        }

        if (device.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out buttonY) && buttonY)
        {
            TeleportTo(new Vector3(-10, 0, 0));
        }

        if (buttonA && buttonX)
        {
            TeleportTo(new Vector3(10, 10, 0));
        }
    }

    void TeleportTo(Vector3 position)
    {
        characterController.enabled = false;
        characterController.transform.position = position;
        characterController.enabled = true;
    }
}
