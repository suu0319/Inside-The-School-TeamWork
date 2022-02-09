using System.Collections;

using System.Collections.Generic;

using UnityEngine;

 

public class HP : MonoBehaviour {
    //最大血量
    public const int MaxHP = 100;
    //血量
    public static int currentHP = MaxHP;
    //血條UI
    public RectTransform HealthBar,Hurt;

    //血條UI扣損
    void Update()
    {      
        HealthBar.sizeDelta = new Vector2(currentHP, HealthBar.sizeDelta.y);
    
        if (Hurt.sizeDelta.x > HealthBar.sizeDelta.x)
        {
            Hurt.sizeDelta += new Vector2(-1, 0);
        }
    }
}