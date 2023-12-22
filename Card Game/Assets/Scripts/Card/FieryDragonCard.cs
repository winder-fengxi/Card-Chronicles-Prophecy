using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieryDragonCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            Debug.Log("FieryDragon");
            Vector3 pos = FollowerManeger.Instance.CreateFollower("10001");
            PlayEffect(pos, 0);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}