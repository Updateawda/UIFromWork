using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIType
{
    Null = 0,
    Fixed,
    Normal,
    Tips,
    Reverse,
    CloseAll,
    Max = 99
}

public class UIBase : MonoBehaviour {

    public UIType uiType;

    //初始化方法
    public virtual void Init()
    {

    }
    //显示方法
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    //隐藏方法
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

}
