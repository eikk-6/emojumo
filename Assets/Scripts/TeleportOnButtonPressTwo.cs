using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportOnButtonPressTwo : MonoBehaviour
{
    InputDevice left;
    InputDevice right;

    [SerializeField]
    GameObject Leftcontroller;
    [SerializeField]
    GameObject Rightcontroller;

    [SerializeField]
    GameObject effectPrefab;  // 출력할 이펙트 프리팹

    public List<int> magicList = new List<int>();
    private int MaxNum = 3;

    public int magicID = 0; // 마법의 고유 번호 

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

        // 초기 magicList를 [0,0,0]으로 설정
        magicList.Add(0);
        magicList.Add(0);
        magicList.Add(0);
    }

    void Update()
    {
        ButtonInput();
    }

    void ButtonInput()
    {
        // 좌우 디바이스 인식
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        // 순차적으로 A, B, X, Y의 버튼 인식 및 작동
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
            TriggerPressed();
        }
    }

    void AddInput(int input)
    {
        // 리스트가 최대 크기보다 크면 처음 입력된 값을 제거합니다
        if (magicList.Count >= MaxNum)
        {
            magicList.RemoveAt(0);
        }
        // 새로운 입력 값을 리스트에 추가합니다
        magicList.Add(input);

        magicID = ConvertListToInt(magicList);
    }

    // 리스트의 숫자를 정수형으로 변경
    int ConvertListToInt(List<int> list)
    {
        int result = 0;
        foreach (int num in list)
        {
            result = result * 10 + num;
        }
        return result;
    }

    void TriggerPressed()
    {
        if (magicID == 1) // [0,0,1]일 때의 magicID는 1임
        {
            Instantiate(effectPrefab, transform.position, transform.rotation);
        }
        // 다른 magicID 값에 따른 다른 이펙트를 추가할 수 있음
    }

    public int IDProvider()
    {
        return magicID;
    }
}
