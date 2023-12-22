using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PandaCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            Debug.Log("Panda");
            Vector3 pos = FollowerManeger.Instance.CreateFollower("10004");
            PlayEffect(pos, 0);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}