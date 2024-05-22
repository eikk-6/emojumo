using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicUiManager : MonoBehaviour
{
    [SerializeField]
    Material Red;
    [SerializeField]
    Material Blue;
    [SerializeField]
    Material Green;
    [SerializeField]
    Material White;

    [SerializeField]


    public Image Que_1;
    public Image Que_2;
    public Image Que_3;

    
    void Start()
    {
        Material Firstmat = Que_1.GetComponent<Material>();
        Material Secmat = Que_2.GetComponent<Material>();
        Material Thremat = Que_3.GetComponent<Material>();
        List<int> XRrigList;
    }

    
    void Update()
    {
        
    }
}
