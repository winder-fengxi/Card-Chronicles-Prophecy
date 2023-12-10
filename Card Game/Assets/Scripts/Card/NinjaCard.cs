using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NinjaCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            Debug.Log("Ninja");
            Vector3 pos = FollowerManeger.Instance.CreateFollower("10003");
            PlayEffect(pos, 0);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}