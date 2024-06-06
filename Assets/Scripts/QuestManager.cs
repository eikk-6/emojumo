using UnityEngine;
using UnityEngine.XR;

public class QuestManager : MonoBehaviour
{
    public GameObject horse; // 말 오브젝트
    private InputDevice rightController;
    private InputDevice leftController;

    void Start()
    {
        // 초기에는 말 오브젝트 비활성화
        horse.SetActive(false);

        // 우측 컨트롤러 인식
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // 트리거 버튼 입력 확인
        rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool isRTriggerPressed);

        if (isRTriggerPressed)
        {
            Vector3 controllerPosition;
            rightController.TryGetFeatureValue(CommonUsages.devicePosition, out controllerPosition);

            Collider buttonCollider = GetComponent<Collider>();
            if (buttonCollider.bounds.Contains(controllerPosition))
            {
                ActivateHorse();
            }
        }
        leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool isLTriggerPressed);

        if (isLTriggerPressed)
        {
            Vector3 controllerPosition;
            leftController.TryGetFeatureValue(CommonUsages.devicePosition, out controllerPosition);

            Collider buttonCollider = GetComponent<Collider>();
            if (buttonCollider.bounds.Contains(controllerPosition))
            {
                ActivateHorse();
            }
        }
    }

    void ActivateHorse()
    {
        // 말 오브젝트 활성화
        horse.SetActive(true);
    }
}
