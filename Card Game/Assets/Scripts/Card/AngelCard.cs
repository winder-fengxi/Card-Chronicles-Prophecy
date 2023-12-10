using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class AngelCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            Debug.Log("Angel");
            Vector3 pos = FollowerManeger.Instance.CreateFollower("10005");
            PlayEffect(pos, 0);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}