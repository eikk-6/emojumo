using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicUiManager : MonoBehaviour
{

    public Material[] mat = new Material[4];


    [SerializeField]
    GameObject XRrig;

    public List<int> XRrigList;

     Material Firstmat;
     Material Secmat;
     Material Thremat;

    public GameObject Que_1;
    public GameObject Que_2;
    public GameObject Que_3;

    
    void Start()
    {
        Firstmat = Que_1.GetComponent<MeshRenderer>().material;
        Secmat = Que_2.GetComponent<MeshRenderer>().material;
        Thremat = Que_3.GetComponent<MeshRenderer>().material;

        XRrigList = XRrig.GetComponent<TeleportOnButtonPressTwo>().magicList;

        Que_1.GetComponent<MeshRenderer>().material = mat[0];
        Que_2.GetComponent<MeshRenderer>().material = mat[0];
        Que_3.GetComponent<MeshRenderer>().material = mat[0];

    }

    
    void Update()
    {
        ShowMagicList();
    }

    void ShowMagicList()
    {
        for (int i = 0; i < 3; i++)
        {
            if(i==0)
            {
                if (XRrigList[i] == 0)
                {
                    Que_1.GetComponent<MeshRenderer>().material = mat[0];
                }
                else if (XRrigList[i] == 1)
                {
                    Que_1.GetComponent<MeshRenderer>().material = mat[1];
                }
                else if (XRrigList[i] == 2)
                {
                    Que_1.GetComponent<MeshRenderer>().material = mat[2];
                }
                else if (XRrigList[i] == 3)
                {
                    Que_1.GetComponent<MeshRenderer>().material = mat[3];
                }
            }

            if (i == 1)
            {
                if (XRrigList[i] == 0)
                {
                    Que_2.GetComponent<MeshRenderer>().material = mat[0];
                }
                else if (XRrigList[i] == 1)
                {
                    Que_2.GetComponent<MeshRenderer>().material = mat[1];
                }
                else if (XRrigList[i] == 2)
                {
                    Que_2.GetComponent<MeshRenderer>().material = mat[2];
                }
                else if (XRrigList[i] == 3)
                {
                    Que_2.GetComponent<MeshRenderer>().material = mat[3];
                }
            }

            if (i == 2)
            {
                if (XRrigList[i] == 0)
                {
                    Que_3.GetComponent<MeshRenderer>().material = mat[0];
                }
                else if (XRrigList[i] == 1)
                {
                    Que_3.GetComponent<MeshRenderer>().material = mat[1];
                }
                else if (XRrigList[i] == 2)
                {
                    Que_3.GetComponent<MeshRenderer>().material = mat[2];
                }
                else if (XRrigList[i] == 3)
                {
                    Que_3.GetComponent<MeshRenderer>().material = mat[3];
                }
            }
        }
    }
}
