using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class TeleportOnButtonPressThree : MonoBehaviour
{
    public XRNode inputSource;
    private InputDevice device;
    private CharacterController characterController;

    private int buttonSequenceIndex = 0;
    private float sequenceTimer = 0.0f;
    private float sequenceTimeout = 3.0f;
    private bool sequenceActive = false;

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
            if (CheckSequence(buttonSequenceIndex == 0))
                buttonSequenceIndex++;
            else
                ResetSequence();
        }

        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonB) && buttonB)
        {
            if (CheckSequence(buttonSequenceIndex == 1))
                buttonSequenceIndex++;
            else
                ResetSequence();
        }

        if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out buttonX) && buttonX)
        {
            if (CheckSequence(buttonSequenceIndex == 2))
                buttonSequenceIndex++;
            else
                ResetSequence();
        }

        if (device.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out buttonY) && buttonY)
        {
            TeleportTo(new Vector3(-10, 10, 0));
        }

        if (sequenceActive)
        {
            sequenceTimer += Time.deltaTime;
            if (sequenceTimer > sequenceTimeout)
            {
                ResetSequence();
            }
        }

        if (buttonA && buttonX)
        {
            TeleportTo(new Vector3(10, 10, 0));
        }
    }

    bool CheckSequence(bool condition)
    {
        if (condition)
        {
            if (!sequenceActive)
            {
                sequenceActive = true;
                sequenceTimer = 0.0f;
            }
            return true;
        }
        return false;
    }

    void ResetSequence()
    {
        buttonSequenceIndex = 0;
        sequenceActive = false;
        sequenceTimer = 0.0f;
    }

    void TeleportTo(Vector3 position)
    {
        characterController.enabled = false;
        characterController.transform.position = position;
        characterController.enabled = true;
    }
}
