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

    private GameObject effectPrefab_1;  // ����� ����Ʈ ������
    private GameObject effectPrefab_2;
    private GameObject effectPrefab_3;
    private GameObject effectPrefab_4;
    private GameObject effectPrefab_5;
    private GameObject effectPrefab_6;
    private GameObject effectPrefab_7;
    private GameObject effectPrefab_8;
    private GameObject effectPrefab_9;
    private GameObject effectPrefab_10;

    public List<int> magicList = new List<int>();
    private int MaxNum = 3;

    public int magicID = 0; // ������ ���� ��ȣ 

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

        // �ʱ� magicList�� [0,0,0]���� ����
        magicList.Add(0);
        magicList.Add(0);
        magicList.Add(0);

        // ������ �ε�
        effectPrefab_1 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane");
        effectPrefab_2 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_2");
        effectPrefab_3 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_3");
        effectPrefab_4 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_4");
        effectPrefab_5 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_5");
        effectPrefab_6 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_6");
        effectPrefab_7 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_7");
        effectPrefab_8 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_8");
        effectPrefab_9 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_9");
        effectPrefab_10 = Resources.Load<GameObject>("SpellsPack/Particles/Prefabs/Spells/Spell_Arcane_10");
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

        // ���������� A, B, X, Y�� ��ư �ν� �� �۵�
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

    void TriggerPressed()
    {
        if (magicID >= 1 && magicID <= 10)
        {
            int index1 = (magicID - 1) / 3;
            int index2 = (magicID - 1) % 3;

            GameObject VFX = null;
            switch (index1)
            {
                case 0:
                    VFX = Instantiate(effectPrefab_1, transform.position, transform.rotation);
                    break;
                case 1:
                    VFX = Instantiate(effectPrefab_2, transform.position, transform.rotation);
                    break;
                case 2:
                    VFX = Instantiate(effectPrefab_3, transform.position, transform.rotation);
                    break;
                case 3:
                    VFX = Instantiate(effectPrefab_4, transform.position, transform.rotation);
                    break;
                case 4:
                    VFX = Instantiate(effectPrefab_5, transform.position, transform.rotation);
                    break;
                case 5:
                    VFX = Instantiate(effectPrefab_6, transform.position, transform.rotation);
                    break;
                case 6:
                    VFX = Instantiate(effectPrefab_7, transform.position, transform.rotation);
                    break;
                case 7:
                    VFX = Instantiate(effectPrefab_8, transform.position, transform.rotation);
                    break;
                case 8:
                    VFX = Instantiate(effectPrefab_9, transform.position, transform.rotation);
                    break;
                case 9:
                    VFX = Instantiate(effectPrefab_10, transform.position, transform.rotation);
                    break;

            }

            if (VFX != null)
            {
                Destroy(VFX, VFX.GetComponent<ParticleSystem>().main.duration);
            }
        }
    }

    public int IDProvider()
    {
        return magicID;
    }
}
