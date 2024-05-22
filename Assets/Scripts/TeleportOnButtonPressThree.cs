using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class TeleportOnButtonPressThree : MonoBehaviour
{
    InputDevice left;
    InputDevice right;
    private CharacterController characterController;

    private int buttonSequenceIndex = 0;
    private float sequenceTimer = 0.0f;
    private float sequenceTimeout = 3.0f;
    private bool sequenceActive = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
       
    }

    void Update()
    {
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        right.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonA);
        if (primaryButtonA)
        {
            if (CheckSequence(buttonSequenceIndex == 0))
                buttonSequenceIndex++;
            else
                ResetSequence();
        }

        
        right.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonB);
        if (secondaryButtonB)
        {
            if (CheckSequence(buttonSequenceIndex == 1))
                buttonSequenceIndex++;
            else
                ResetSequence();
        }

        left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
      
        left.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonX);
        if (primaryButtonX)
        {
            if (CheckSequence(buttonSequenceIndex == 2))
                buttonSequenceIndex++;
            else
                ResetSequence();
        }
        left.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonY);
        if (secondaryButtonY)
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

        if (primaryButtonA && primaryButtonX)
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
