using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportOnButtonPressTwo : MonoBehaviour
{
            //{"triggerButton", CommonUsages.triggerButton }
            //{ "thumbrest", CommonUsages.thumbrest }
            //{ "primary2DAxisClick", CommonUsages.primary2DAxisClick }
            //{ "primary2DAxisTouch", CommonUsages.primary2DAxisTouch }
            //{ "menuButton", CommonUsages.menuButton }
            //{ "gripButton", CommonUsages.gripButton }
            //{ "secondaryButton", CommonUsages.secondaryButton }
            //{ "secondaryTouch", CommonUsages.secondaryTouch }
            //{ "primaryButton", CommonUsages.primaryButton }
            //{ "primaryTouch", CommonUsages.primaryTouch }









    private CharacterController characterController;

    InputDevice left;
    InputDevice right;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        RightTriggerButton();
        LeftTriggerButton();

        ButtonA();
        ButtonB();



    }

    void RightTriggerButton()
    {
        // needs to be in Update
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        // assigns button value to out variable, if expecting Vector3 replace bool
        right.TryGetFeatureValue(CommonUsages.triggerButton, out bool isRTriggerPressed);
        if (isRTriggerPressed)
        {
            Debug.Log("RtriggerButton");
        }
    }

    void LeftTriggerButton()
    {
        left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        left.TryGetFeatureValue(CommonUsages.triggerButton, out bool isLTriggerPressed);

        if (isLTriggerPressed)
        {
            Debug.Log("LtriggerButton");
            TeleportTo(new Vector3(5, 0, 0));
        }
    }
    void ButtonA()
    {
        // needs to be in Update
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        // assigns button value to out variable, if expecting Vector3 replace bool
        right.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonA);
        if (primaryButtonA)
        {
            Debug.Log("ButtonA"); 
        }
    }
    void ButtonB()
    {
        // needs to be in Update
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        // assigns button value to out variable, if expecting Vector3 replace bool
        right.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonY);
        if (secondaryButtonY)
        {
            Debug.Log("ButtonB ");
        }
    }
    void ButtonX()
    {
        // needs to be in Update
        left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        // assigns button value to out variable, if expecting Vector3 replace bool
        left.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonX);
        if (primaryButtonX)
        {
            Debug.Log("ButtonX");
        }
    }
    void ButtonY()
    {
        // needs to be in Update
        left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        // assigns button value to out variable, if expecting Vector3 replace bool
        left.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonY);
        if (secondaryButtonY)
        {
            Debug.Log("ButtonY");
        }
    }


    void TeleportTo(Vector3 position)
    {
        characterController.enabled = false;
        characterController.transform.position = position;
        characterController.enabled = true;
    }
}