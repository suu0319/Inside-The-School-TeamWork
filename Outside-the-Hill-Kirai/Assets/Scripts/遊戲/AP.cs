using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AP : MonoBehaviour
{
    //最大AP數值
    public float MaxAP;
    //AP數值
    public static float currentAP = 100;
    //MPUI
    public Image Bar;

    // Update is called once per frame
    void Update()
    {
        //同步APUI = AP
        Bar.fillAmount = currentAP/MaxAP;
    }
}
