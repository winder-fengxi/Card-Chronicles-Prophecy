using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clone : CardItem, IPointerDownHandler
{

    //按下
    public void OnPointerDown(PointerEventData eventData)
    {
        if(FightManager.Instance.EnergyCardcheck>0||FightManager.Instance.AttackCardcheck>0)
        {
            //播放声音
            AudioManager.Instance.PlayEffect("Cards/draw");
            //显示曲线界面
            UIManager.Instance.ShowUI<LineUI>("LineUI");
            //设置开始点位置
            UIManager.Instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);
            //隐藏鼠标
            Cursor.visible = false;
            //关闭所有协同程序
            StopAllCoroutines();
            //启动鼠标操作协同程序
            StartCoroutine(OnMouseDownRight(eventData));
        }
        else
        {
            UIManager.Instance.ShowTip("卡牌中没有能力卡或者攻击卡，无法使用", Color.red, delegate ()
            {});
        }

    }
    IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {
            //如果按下鼠标右键跳出循环
            if (Input.GetMouseButton(1)) break;
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                pData.position,
                pData.pressEventCamera,
                out pos
            ))
            {
                //设置箭头位置
                UIManager.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);
                // 进行射线检测是否碰到卡牌
                CheckRayToEnemy();
            }

            yield return null;
        }
        Cursor.visible = true;
        //关闭曲线界面
        UIManager.Instance.CloseUI("LineUI");
    }
    CardItem chooseCard;//射线检测到的卡牌脚本
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("AttackCard"))|| Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("EnergyCard")))
        {
            chooseCard = hit.transform.GetComponent<CardItem>();
            chooseCard.OnSelect();//选中
                                //如果按下鼠标左键使用攻击卡
            if (Input.GetMouseButtonDown(0))
            {
                //关闭所有协同程序
                StopAllCoroutines();
                //鼠标显示
                Cursor.visible = true;
                //关闭曲线界面
                UIManager.Instance.CloseUI("LineUI");
                if (TryUse() == true)
                {
                    //复制卡牌
                    UIManager.Instance.GetUI<FightUI>("FightUI").CopyCardItem(chooseCard);
                }
                //卡牌未选中
                chooseCard.OnUnSelect();
                //设置卡牌脚本null
                chooseCard = null;
            }
        }
        else
        {
            //未射到卡牌
            if (chooseCard != null)
            {
                chooseCard.OnUnSelect();
                chooseCard = null;
            }

        }

    }
}