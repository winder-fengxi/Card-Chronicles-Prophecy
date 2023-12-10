using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public enum ActionType
{
    None,
    Defend,//加防御
    Attack,//攻击
}

//敌人脚本
public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//敌人数据表信息
    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //UI相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //数值相关
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //血量、防御UI的检测
    public bool Defendcheck = false;
    public bool Attackcheck = false;

    SkinnedMeshRenderer _meshrenderer;

    //public Animator ani;

    //检测是否易伤状态
    public bool easyhit = false;

    //检测是否受到“神威！”
    public bool ShenweiCheck = false;
    public bool ShenweiCheckUse = false;

    //中毒层数
    public int poisonnum = 0;
    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    void Start()
    {
        //ani = transform.GetComponent<Animator>();
        _meshrenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();

        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();
        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");
        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();
        // 设置血条行动力位置
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up*2);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down);
        SetRandomAction();
        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);

        UpdateHp();
        UpdateDefend();
    }

    //隐藏敌人的UI
    public void HideUI()
    {
        if (Attackcheck)
        {
            attackTf.gameObject.SetActive(false);
        }
        else
        {
            defendTf.gameObject.SetActive(false);
        }
        hpItemObj.gameObject.SetActive(false);
        actionObj.gameObject.SetActive(false);
    }

    //显示敌人的UI
    public void DisplayUI()
    {
        hpItemObj.gameObject.SetActive(true);
        actionObj.gameObject.SetActive(true);
    }


    //随机一个行动
    public void SetRandomAction()
    {
        int ran = UnityEngine.Random.Range(1, 3);
        type = (ActionType)ran;
        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                Attackcheck = false;
                Defendcheck = true;
                attackTf.gameObject.SetActive(Attackcheck);
                defendTf.gameObject.SetActive(Defendcheck);
                break;
            case ActionType.Attack:
                Attackcheck = true;
                Defendcheck = false;
                attackTf.gameObject.SetActive(Attackcheck);
                defendTf.gameObject.SetActive(Defendcheck);
                break;
        }
    }

    //更新血量信息
    public void UpdateHp()
    {
        hpTxt.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //更新防御信息
    public void UpdateDefend()
    {
        defendTxt.text = Defend.ToString();
    }

    //被攻击卡选中，显示红边
    public void OnSelect()
    {
        _meshrenderer.material.SetColor("_OtlColor", Color.red);
    }

    //未选中
    public void OnUnSelect()
    {
        _meshrenderer.material.SetColor("_OtlColor", Color.black);
    }

    //受伤
    public void Hit(int val)
    {
        //敌人处于易伤状态
        if(easyhit)
        {
            val++;
        }
        //先扣护盾
        if (Defend > val)
        {
            //扣护盾
            Defend -= val;
            //播放受伤
            //ani.Play("hit", 0, 0);
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if (CurHp <= 0)
            {
                CurHp = 0;
                // 播放死亡
                //ani.Play("die");
                //敌人从列表中移除
                EnemyManeger.Instance.DeleteEnemy(this);
                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //受伤
                //ani.Play("hit", 0, 0);
            }
        }
        //刷新血量等ui
        UpdateDefend();
        UpdateHp();
    }

    //隐藏怪物头上的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    //执行敌人行动
    public IEnumerator DoAction()
    {
        if (ShenweiCheck == false)
        {
            HideAction();
            //播放对应的动画（可以配置到excel表这里都默认播放攻击）
            //ani.Play("attack");
            //等待某一时间的后执行对应的行为（也可以配置到excel表）
            yield return new WaitForSeconds(0.5f);//这里我写死了
            switch (type)
            {
                case ActionType.None:
                    break;
                case ActionType.Defend:
                    // 加防御
                    Defend += 1;
                    UpdateDefend();
                    //可以播放对应的特效
                    break;
                case ActionType.Attack:
                    // 玩家扣血
                    FightManager.Instance.GetPlayerHit(Attack);
                    //摄像机可以抖一抖
                    Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                    break;
            }
            //等待动画播放完（这里的时长也可以配置）
            yield return new WaitForSeconds(1);
            //播放待机
            //ani.Play("idle");
        }
        else
        {
            ShenweiCheckUse = true;
        }
    }

    //private int index;
    ////鼠标进入
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if(EnemyManeger.Instance.chooseEnemy>0)
    //    {
    //        transform.DOScale(1.5f, 0.25f);
    //        index = transform.GetSiblingIndex();
    //        transform.SetAsLastSibling();
    //        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
    //        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    //    }
    //}

    ////鼠标离开
    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (EnemyManeger.Instance.chooseEnemy > 0)
    //    {
    //        transform.DOScale(1, 0.25f);
    //        transform.SetSiblingIndex(index);
    //        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
    //        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    //    }
    //}

    ////选择敌人
    //public virtual Enemy SeEnemy()
    //{
    //    if (EnemyManeger.Instance.chooseEnemy > 0)
    //    {

    //        return this;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
}