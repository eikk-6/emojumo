using System.Collections;
using System.Collections.Generic;
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

    InputDevice left;
    InputDevice right;

    [SerializeField]
    GameObject Leftcontroller;
    [SerializeField]
    GameObject Rightcontroller;

    public List<int> magicList = new List<int>();
    private int MaxNum = 3;

    public int magicID = 0; //  ������ ���� ��ȣ 

    private bool isButtonA = false;
    private bool isButtonB = false;
    private bool isButtonX = false;
    private bool isButtonY = false;
    
    private bool prevButtonA;
    private bool prevButtonB;
    private bool prevButtonX;
    private bool prevButtonY;

    private int BtnNumA = 0;
    private int BtnNumB = 1;
    private int BtnNumX = 2;
    private int BtnNumY = 3;

    void Start()
    {
        prevButtonA = false;
        prevButtonB = false;
        prevButtonX = false;
        prevButtonY = false;
    }

    void Update()
    {
        ButtonInput();

    }

    void ButtonInput()
    {
        // �¿� ����̽� �ν�
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        // ���������� A,B,X,Y�� ��ư �ν� �� �۵�
        right.TryGetFeatureValue(CommonUsages.primaryButton, out isButtonA);

        if (isButtonA && !prevButtonA)
        {
            AddInput(BtnNumA);
        }
        prevButtonA = isButtonA;

        right.TryGetFeatureValue(CommonUsages.secondaryButton, out isButtonB);
        if (isButtonB && !prevButtonB)
        {
            AddInput(BtnNumB);
        }
        prevButtonB = isButtonB;

        left.TryGetFeatureValue(CommonUsages.primaryButton, out isButtonX);
        if (isButtonX && !prevButtonX)
        {
            AddInput(BtnNumX);
        }
        prevButtonX = isButtonX;

        left.TryGetFeatureValue(CommonUsages.secondaryButton, out isButtonY);
        if (isButtonY && !prevButtonY)
        {
            AddInput(BtnNumY);
        }
        prevButtonY = isButtonY;


        right.TryGetFeatureValue(CommonUsages.triggerButton, out bool isRTriggerPressed);
        left.TryGetFeatureValue(CommonUsages.triggerButton, out bool isLTriggerPressed);
        if (isRTriggerPressed || isLTriggerPressed)
        {
            // Ʈ���Ÿ� ������ magicID�� �ش��ϴ� ���� ȿ�� �Ǵ� ����ü �߻� �Լ� ���� 
        }
    }

    void AddInput(int input)
    {
        // ����Ʈ�� �ִ� ũ�⺸�� ũ�� ó�� �Էµ� ���� �����մϴ�
        if (magicList.Count >= MaxNum)
        {
            magicList.RemoveAt(0);
        }
        // ���ο� �Է� ���� ����Ʈ�� �߰��մϴ�
        magicList.Add(input);

        magicID = ConvertListToInt(magicList);
    }

    // ����Ʈ�� ���ڸ� ���������� ����
    int ConvertListToInt(List<int> list)    
    {
        int result = 0;
        foreach (int num in list)
        {
            result = result * 10 + num;
        }
        return result;
    }

    public int IDProvider()
    {
        return magicID;
    }
    
}