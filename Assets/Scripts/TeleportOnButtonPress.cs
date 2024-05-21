using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportOnButtonPress : MonoBehaviour
{
    public XRNode leftHandNode = XRNode.LeftHand;
    public XRNode rightHandNode = XRNode.RightHand;
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;

    private void Start()
    {
        InitializeDevices();
    }

    private void InitializeDevices()
    {
        leftHandDevice = InputDevices.GetDeviceAtXRNode(leftHandNode);
        rightHandDevice = InputDevices.GetDeviceAtXRNode(rightHandNode);
    }

    private void Update()
    {
        if (!leftHandDevice.isValid || !rightHandDevice.isValid)
        {
            InitializeDevices();
        }

        CheckButtonPress(leftHandDevice, rightHandDevice);
    }

    private void CheckButtonPress(InputDevice leftDevice, InputDevice rightDevice)
    {
        if (leftDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            Teleport(new Vector3(10, 0, 0));
        }
        if (leftDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
        {
            Teleport(new Vector3(-10, 0, 0));
        }
        if (rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue)
        {
            Teleport(new Vector3(0, 0, 10));
        }
        if (rightDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue)
        {
            Teleport(new Vector3(0, 0, -10));
        }
    }

    private void Teleport(Vector3 newPosition)
    {
        Transform playerTransform = Camera.main.transform;
        Vector3 teleportPosition = newPosition;

        teleportPosition.y = playerTransform.position.y;

        playerTransform.position = teleportPosition;
    }
}

