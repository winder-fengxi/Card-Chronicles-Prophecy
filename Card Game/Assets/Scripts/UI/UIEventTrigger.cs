using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

//事件监听
public class UIEventTrigger : MonoBehaviour, IPointerClickHandler
{
    //这是一个公共的委托，它接受两个参数，一个是被点击的游戏对象，另一个是关于点击事件的数据。
    public Action<GameObject, PointerEventData> onClick;

    //用于获取或添加 UIEventTrigger 组件
    public static UIEventTrigger Get(GameObject obj)
    {
        UIEventTrigger trigger = obj.GetComponent<UIEventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<UIEventTrigger>();
        }
        return trigger;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //这是 IPointerClickHandler 接口的方法，当 UI 元素被点击时，它将被调用。
        if (onClick != null) onClick(gameObject, eventData);
    }
}