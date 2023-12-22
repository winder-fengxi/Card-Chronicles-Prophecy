using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class thunder : CardItem, IPointerDownHandler
{
    private int count = 0;
    private bool Hitcheck = false;
    //按下
    public void OnPointerDown(PointerEventData eventData)
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
                // 进行射线检测是否碰到怪物
                CheckRayToEnemy(pData);
            }

            yield return null;
        }
        Cursor.visible = true;
        //关闭曲线界面
        UIManager.Instance.CloseUI("LineUI");

        if (Hitcheck == false)
        {
            OnPointerDown(pData);
        }
    }
    Enemy[] hitEnemy = new Enemy[2];//射线检测到的敌人脚本
    private void CheckRayToEnemy(PointerEventData pData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            hitEnemy[count] = hit.transform.GetComponent<Enemy>();
            hitEnemy[count].OnSelect();//选中

            //如果按下鼠标左键使用攻击卡
            if (Input.GetMouseButtonDown(0))
            {
                count++;
            }

            //如果按下鼠标左键两次使用雷电卡,少于则继续选择
            if (count == 1 && FightManager.Instance.Raincheck == false)
            {
                //关闭所有协同程序
                StopAllCoroutines();
                //鼠标显示
                Cursor.visible = true;
                //关闭曲线界面
                UIManager.Instance.CloseUI("LineUI");
                if (TryUse() == true)
                {
                    //播放特效
                    PlayEffect(hitEnemy[0].transform.position, 0);

                    UIManager.Instance.GetUI<FightUI>("FightUI").UpdateWeather("pictrue/Buff/small_light");
                    //打击音效
                    AudioManager.Instance.PlayEffect("Effect/sword");
                    //敌人受伤
                    int val = 5;

                    //若本轮使用过“圣歌”卡，则本轮攻击值+1
                    if (FightManager.Instance.Encouragcheck != 0)
                    {
                        val += FightManager.Instance.Encouragcheck;
                    }
                    //若本轮使用过“断头台”，则进入恐慌转台，本轮攻击值+1
                    if (FightManager.Instance.EnemyPaniccheck > 0)
                    {
                        val += 1;
                    }
                    Debug.Log("攻击值 = " + val);
                    hitEnemy[0].Hit(val);
                    count--;
                }
                Hitcheck = true;
                //敌人未选中
                hitEnemy[0].OnUnSelect();
                //设置敌人脚本null
                hitEnemy[0] = null;
            }
            else if (count == 2)
            {
                //关闭所有协同程序
                StopAllCoroutines();
                //鼠标显示
                Cursor.visible = true;
                //关闭曲线界面
                UIManager.Instance.CloseUI("LineUI");
                if (TryUse() == true)
                {
                    //播放特效
                    PlayEffect(hitEnemy[0].transform.position, 0);
                    PlayEffect(hitEnemy[1].transform.position, 0);
                    //打击音效
                    AudioManager.Instance.PlayEffect("Effect/sword");
                    //敌人受伤
                    int val = int.Parse(data["ArgO"]);
                    val *= 2;

                    //若本轮使用过“圣歌”卡，则本轮攻击值+1
                    if (FightManager.Instance.Encouragcheck != 0)
                    {
                        val += FightManager.Instance.Encouragcheck;
                    }
                    //若本轮使用过“断头台”，则进入恐慌转台，本轮攻击值+1
                    if (FightManager.Instance.EnemyPaniccheck > 0)
                    {
                        val += 1;
                    }
                    Debug.Log("攻击值 = " + val);
                    hitEnemy[0].Hit(val);
                    hitEnemy[1].Hit(val);
                    count--;
                }
                //敌人未选中
                hitEnemy[1].OnUnSelect();
                //设置敌人脚本null
                hitEnemy[1] = null;
            }

            Debug.Log("count = " + count);
        }
        else
        {
            //未射到怪物
            if (hitEnemy[count] != null)
            {
                hitEnemy[count].OnUnSelect();
                hitEnemy[count] = null;
            }

        }

    }
}