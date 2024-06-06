using UnityEngine;
using UnityEngine.XR;

public class QuestManager : MonoBehaviour
{
    public GameObject horse; // �� ������Ʈ
    private InputDevice rightController;
    private InputDevice leftController;

    void Start()
    {
        // �ʱ⿡�� �� ������Ʈ ��Ȱ��ȭ
        horse.SetActive(false);

        // ���� ��Ʈ�ѷ� �ν�
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // Ʈ���� ��ư �Է� Ȯ��
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
        // �� ������Ʈ Ȱ��ȭ
        horse.SetActive(true);
    }
}
