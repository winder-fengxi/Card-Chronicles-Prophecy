using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class untie : CardItem, IPointerDownHandler
{

    //按下
    public void OnPointerDown(PointerEventData eventData)
    {
        if(FollowerManeger.Instance.followerList.Count>0)
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
            UIManager.Instance.ShowTip("场上没有随从可选择", Color.red, delegate ()
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
                // 进行射线检测是否碰到随从
                CheckRayToEnemy();
            }

            yield return null;
        }
        Cursor.visible = true;
        //关闭曲线界面
        UIManager.Instance.CloseUI("LineUI");
    }
    Follower chooseFollower;//射线检测到的随从脚本
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit choose;
        if (Physics.Raycast(ray, out choose, 10000, LayerMask.GetMask("Follower")))
        {
            chooseFollower = choose.transform.GetComponent<Follower>();
            chooseFollower.OnSelect();//选中
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
                    Vector3 pos = chooseFollower.transform.position;
                    PlayEffect(pos, 0);
                    //增加精力
                    FightManager.Instance.CurPowerCount += 1;
                    // 刷新精力文本
                    UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
                }
                //随从未选中
                chooseFollower.OnUnSelect();
                //设置随从脚本null
                chooseFollower = null;
            }
        }
        else
        {
            //未射到随从
            if (chooseFollower != null)
            {
                chooseFollower.OnUnSelect();
                chooseFollower = null;
            }

        }

    }
}